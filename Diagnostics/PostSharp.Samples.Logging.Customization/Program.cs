using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Audit;
using System;
using PostSharp.Samples.Logging.BusinessLogic;

[assembly: Log]

namespace PostSharp.Samples.Logging
{
    [Log(AttributeExclude = true)]
    static class Program
    {
        
        static void Main(string[] args)
        {
            // Register the custom logging backend.
            var backend = new CustomLoggingBackend();
            LoggingServices.DefaultBackend = backend ;

            // Register the custom parameter formatter.
            LoggingServices.Formatters.Register(new FancyIntFormatter());


            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");

            ExampleFormattable.Greet( new ExampleFormattable { FirstName = "Yuri", LastName = "Gagarin"} );

        }

    }
}
