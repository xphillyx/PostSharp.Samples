using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;
using PostSharp.Samples.Logging.BusinessLogic;

[assembly: Log]

namespace PostSharp.Samples.Logging.Console
{
    [Log(AttributeExclude = true)]
    class Program
    {
        static void Main(string[] args)
        {
            var backend = new ConsoleLoggingBackend();
            
            LoggingServices.DefaultBackend = backend;

            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
        }
    }
}
