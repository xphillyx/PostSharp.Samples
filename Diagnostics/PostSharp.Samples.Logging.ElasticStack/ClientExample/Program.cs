using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Serilog;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using Serilog;
using Serilog.Sinks.Http.BatchFormatters;

[assembly: Log]

namespace ClientExample
{
    public class Program
    {
      
        static async Task Main()
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .Enrich.WithProperty( "Application", "ClientExample" )
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Async( a => a.DurableHttp( requestUri: "http://localhost:31311", batchFormatter: new ArrayBatchFormatter(), batchPostingLimit: 5 ) )
                .WriteTo.Console( outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Indent:l}{Message:l}{NewLine}{Exception}" )
                .CreateLogger();


            SerilogLoggingBackend backend = new SerilogLoggingBackend( logger );
            backend.Options.IncludeExceptionDetails = true;
            backend.Options.SemanticParametersTreatedSemantically |= SemanticParameterKind.MemberName;
            LoggingServices.DefaultBackend = backend;

            using ( logger )
            {
                await QueueProcessor.ProcessQueue( ".\\My\\Queue" );
            }

            Console.WriteLine( "Done!" );
        }

    }
}
