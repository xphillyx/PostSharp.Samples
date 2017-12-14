using System;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends;
using PostSharp.Samples.Logging.BusinessLogic;
using LogLevel = NLog.LogLevel;

// Apply logging to the whole assembly.
[assembly: Log]

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  internal class Program
  {
    private static int testMethodCounter;

    private static async Task Main(string[] args)
    {
      // Test that LoggingCircuitBreaker works
      TestMethodThrowingException();
      TestMethodThrowingException();
      if (testMethodCounter != 1)
      {
        Console.Error.WriteLine("LoggingCircuitBreaker aspect does not work.");
        return;
      }
      LoggingCircuitBreaker.Reset();

      // Configure NLog.
      var nlogConfig = new LoggingConfiguration();

      var fileTarget = new FileTarget("file")
      {
        FileName = "nlog.log",
        KeepFileOpen = true,
        ConcurrentWrites = false
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
      Console.WriteLine("*** ERROR ***   There should be no log lines below.");

      // Simulate some business logic.
      QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
    }

    [LoggingCircuitBreaker]
    private static DateTime TestMethodThrowingException()
    {
      testMethodCounter++;
      throw new Exception();
    }
  }
}