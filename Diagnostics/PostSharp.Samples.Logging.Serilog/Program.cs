using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Serilog;
using PostSharp.Samples.Logging.BusinessLogic;
using Serilog;

[assembly: Log]

namespace PostSharp.Samples.Logging.Serilog
{
    [Log(AttributeExclude = true)]
    class Program
    {
        static Program()
        {
            Logger logger;
        }

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("serilog.log")
                .WriteTo.ColoredConsole()
                .CreateLogger();
            

            var serilogBackend = new SerilogLoggingBackend();
            
            LoggingServices.DefaultBackend = serilogBackend;

            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
        }

        [Log]
        static void M()
        {
            
        }
    }
}
