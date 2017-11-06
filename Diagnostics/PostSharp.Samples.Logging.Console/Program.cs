using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;
using PostSharp.Samples.Logging.BusinessLogic;

[assembly: Log]

namespace PostSharp.Samples.Logging.Console
{
  [Log(AttributeExclude = true)]
  internal class Program
  {
    private static void Main(string[] args)
    {
      // Configure PostSharp Logging to output logs to the console.
      LoggingServices.DefaultBackend = new ConsoleLoggingBackend();

      // Simulate some business logic.
      QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
    }
  }
}