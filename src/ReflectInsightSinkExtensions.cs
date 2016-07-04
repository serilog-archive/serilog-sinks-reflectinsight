// Copyright 2016 Serilog Contributors 
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

using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using System;

namespace Serilog
{
    /// <summary>
    /// 
    /// </summary>
    static public class ReflectInsightSinkExtensions
    {
        /// <summary>
        /// Reflects the insight.
        /// </summary>
        /// <param name="sinkConfiguration">The sink configuration.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="minLevel">The minimum level.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">sinkConfiguration</exception>
        public static LoggerConfiguration ReflectInsight(
            this LoggerSinkConfiguration sinkConfiguration,
            string instance = null,
            LogEventLevel minLevel = LevelAlias.Minimum)
        {
            if (sinkConfiguration == null)
            {
                throw new ArgumentNullException(nameof(sinkConfiguration));
            }

            return sinkConfiguration.Sink(new Serilog.Sinks.ReflectInsightSink(new MessageTemplateTextFormatter("{Message}", null), instance), minLevel);
        }
    }
}
