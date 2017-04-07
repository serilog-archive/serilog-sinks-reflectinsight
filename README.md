# Serilog.Sinks.ReflectInsight

The ReflectInsight sink for Serilog.

[![Build status](https://ci.appveyor.com/api/projects/status/hh9gymy0n6tne46j?svg=true)](https://ci.appveyor.com/project/serilog/serilog-sinks-reflectinsight) 
[![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.ReflectInsight.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.ReflectInsight/)

**Package** - [Serilog.Sinks.ReflectInsight](http://nuget.org/packages/serilog.sinks.reflectinsight) | **Platforms** - .NET 4.5.1

## Overview ##

We've added a Serilog Sink for [ReflectInsight](http://reflectsoftware.com) live log viewer. This allows you to leverage your current investment in Serilog, but leverage the power and flexibility that comes with the ReflectInsight viewer. You can view your Serilog messages in real-time, in a rich viewer that allows you to filter out and search for what really matters to you.

## Getting Started

To install the Reflectinsight Serilog Sink, run the following command in the Package Manager Console:

```powershell
Install-Package Serilog.Sinks.ReflectInsight
```


### Usage

```csharp#
using Serilog;

Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .WriteTo.ReflectInsight()				   
                   .CreateLogger();
```


### Documentation

- Serilog [Documentation](https://github.com/serilog/serilog/wiki)
- Please refer to the ReflectInsight [Documentation](https://reflectsoftware.atlassian.net/wiki/display/RI5/ReflectInsight+5+documentation) for details on configuring ReflectInsight.

Copyright &copy; 2016 Serilog Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html).
