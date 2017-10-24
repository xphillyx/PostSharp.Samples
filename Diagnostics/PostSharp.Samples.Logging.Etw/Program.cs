using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.EventSource;
using PostSharp.Samples.Logging.BusinessLogic;

[assembly: Log]

namespace PostSharp.Samples.Logging.Etw
{
    [Log(AttributeExclude = true)]
    class Program
    {
        static void Main(string[] args)
        {
            var eventSourceBackend = new EventSourceLoggingBackend(new PostSharpEventSource());
            if (eventSourceBackend.EventSource.ConstructionException != null)
                throw eventSourceBackend.EventSource.ConstructionException;

            LoggingServices.DefaultBackend = eventSourceBackend;

            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");

            // To collect and view the log:
            //   1. Download PerfView from https://www.microsoft.com/en-us/download/details.aspx?id=28567.
            //   2. Execute: perfview.exe collect -OnlyProviders *PostSharp-Patterns-Diagnostics
            //   3. Execute this program.
            //   4. In PerfView, click 'Stop collecting', then in the PerfView tree view click 'PerfViewData.etl.zip' and finally 'Events'.
        }
    }
}
