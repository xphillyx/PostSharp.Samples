using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibraltar.Agent;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Loupe;
using PostSharp.Samples.Logging.BusinessLogic;

[assembly: Log]

namespace PostSharp.Samples.Logging.Loupe
{
    [Log(AttributeExclude = true)]
    class Program
    {
        static void Main(string[] args)
        {
            Gibraltar.Agent.Log.StartSession();
            Gibraltar.Agent.Log.ShowLiveViewer();

            // Configure PostSharp Logging to use Loupe.
            LoggingServices.DefaultBackend = new LoupeLoggingBackend();

            // Simulate some business logic.
            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");

            

            
            Console.WriteLine("Press Enter to finish.");
            Console.ReadLine();

            Gibraltar.Agent.Log.EndSession();
        }
    }
}
