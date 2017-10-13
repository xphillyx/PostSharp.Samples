using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Samples.Logging.BusinessLogic;
using LogLevel = NLog.LogLevel;

[assembly: Log]

namespace PostSharp.Samples.Logging.NLog
{
    [Log(AttributeExclude = true)]
    static class Program
    {
        static void Main(string[] args)
        {
            LoggingConfiguration nlogConfig = new LoggingConfiguration();

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



            LoggingServices.DefaultBackend = new NLogLoggingBackend();

            LogManager.Configuration = nlogConfig;
            LogManager.EnableLogging();

            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
        }
    }
}
