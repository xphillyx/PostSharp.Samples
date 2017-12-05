using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends;
using PostSharp.Samples.Logging.BusinessLogic;
using LogLevel = NLog.LogLevel;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{


  [Log(AttributeExclude = true)]
  class Program
  {
    static async Task Main(string[] args)
    {
      // Configure NLog.
      var nlogConfig = new LoggingConfiguration();

      var fileTarget = new FileTarget("file")
      {
        FileName = "nlog.log",
        KeepFileOpen = true,
        ConcurrentWrites = false,
      };

      nlogConfig.AddTarget(fileTarget);
      nlogConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));

      var consoleTarget = new ConsoleTarget("console");
      nlogConfig.AddTarget(consoleTarget);
      nlogConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));

      LogManager.Configuration = nlogConfig;
      LogManager.EnableLogging();

      TextLoggingBackend backend = new CircuitBreakingLoggingBackend();
      LoggingServices.DefaultBackend = backend;

      // Simulate some business logic.
      QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");

      // Simulate an error.
      LoggingCircuitBreaker.Break();
      Console.WriteLine("*** ERROR ***");

      // Simulate some business logic.
      QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");



    }


  }
}
