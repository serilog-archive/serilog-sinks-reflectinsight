// Copyright 2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ReflectSoftware.Insight;
using ReflectSoftware.Insight.Common;
using RI.Utils.ExceptionManagement;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Parsing;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Serilog.Sinks.ReflectInsight
{
    /// <summary>
    /// ReflectInsight Sink
    /// </summary>
    /// <seealso cref="Serilog.Core.ILogEventSink" />
    public class ReflectInsightSink : ILogEventSink
    {
        private readonly ITextFormatter _formatter;
        private readonly string _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectInsightSink"/> class.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <param name="instance">The instance.</param>
        public ReflectInsightSink(ITextFormatter formatter, string instance = null)
        {
            _formatter = formatter;
            _instance = string.IsNullOrWhiteSpace(instance) ? "default" : instance;
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        /// <exception cref="ArgumentNullException">logEvent</exception>
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            var strBuilder = new StringBuilder();
            using (var sr = new StringWriter(strBuilder))
            {
                _formatter.Format(logEvent, sr);
                var message = sr.ToString().Trim();

                if(string.IsNullOrWhiteSpace(message))
                {
                    message = logEvent.MessageTemplate.Text;
                }

                var messageType = MessageType.SendDebug; // default
                switch (logEvent.Level)
                {
                    case LogEventLevel.Debug: messageType = MessageType.SendDebug; break;
                    case LogEventLevel.Verbose: messageType = MessageType.SendVerbose; break;
                    case LogEventLevel.Information: messageType = MessageType.SendInformation; break;
                    case LogEventLevel.Warning: messageType = MessageType.SendWarning; break;
                    case LogEventLevel.Error: messageType = MessageType.SendError; break;
                    case LogEventLevel.Fatal: messageType = MessageType.SendFatal; break;
                }

                var propTokens = logEvent.MessageTemplate.Tokens.Where(x => x is PropertyToken).Select(x => (x as PropertyToken).PropertyName);
                var nonTokenProps = logEvent.Properties.Where(x => !propTokens.Contains(x.Key)).Select(x => x);

                strBuilder.Clear();
                strBuilder.Capacity = 0;

                if (nonTokenProps.Any())
                {
                    sr.WriteLine("\tAdditional Properties:");

                    foreach (var prop in nonTokenProps)
                    {
                        sr.Write($"\t\t{prop.Key}: ");
                        prop.Value.Render(sr);
                        sr.WriteLine();
                    }

                    sr.WriteLine();
                }

                if (logEvent.Exception != null)
                {
                    sr.WriteLine(ExceptionBasePublisher.ConstructIndentedMessage(logEvent.Exception));
                }

                var details = strBuilder.Length > 0 ? strBuilder.ToString() : null;
                RILogManager.Get(_instance).Send(messageType, message, details);
            }
        }
    }
}

