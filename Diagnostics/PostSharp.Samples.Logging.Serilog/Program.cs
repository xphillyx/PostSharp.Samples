using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Serilog;
using PostSharp.Samples.Logging.BusinessLogic;
using Serilog;

// Add logging to all methods of this project.
[assembly: Log]

namespace PostSharp.Samples.Logging.Serilog
{
  [Log(AttributeExclude = true)]   // Removes logging from the Program class itself.
  internal class Program
  {
    private static void Main(string[] args)
    {
      // The output template must include {Indent} for nice output.
      const string template = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Indent:l}{Message}{NewLine}{Exception}";

      // Configure a Serilog logger.
      var logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.File("serilog.log", outputTemplate: template)
        .WriteTo.ColoredConsole(outputTemplate: template)
        .CreateLogger();

      // Configure PostSharp Logging to use Serilog
      LoggingServices.DefaultBackend = new SerilogLoggingBackend(logger);

      // Simulate some business logic.
      QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
    }
  }
}