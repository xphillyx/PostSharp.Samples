using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Serilog;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using Serilog;
using Serilog.Sinks.Http.BatchFormatters;

[assembly: Log]

namespace MicroserviceExample
{
    public class Program
    {
        

        public static void Main( string[] args )
        {
            // Configure Serilog.
            Serilog.Core.Logger logger = new LoggerConfiguration()
              .Enrich.WithProperty( "Application", "MicroserviceExample" )
              .MinimumLevel.Debug()
              .WriteTo.Async( a => a.DurableHttp( requestUri: "http://localhost:31311", batchFormatter: new ArrayBatchFormatter(), batchPostingLimit: 5 ) )
              .WriteTo.Console( outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Indent:l}{Message:l}{NewLine}{Exception}" )
              .CreateLogger();

            // Configure PostSharp.
            SerilogLoggingBackend backend = new SerilogLoggingBackend( logger );
            backend.Options.IncludeExceptionDetails = true;
            backend.Options.SemanticParametersTreatedSemantically |= SemanticParameterKind.MemberName;
            LoggingServices.DefaultBackend = backend;
            LoggingServices.Formatters.Register( typeof( ActionResult<> ), typeof( ActionResultFormatter<> ) );
            LoggingServices.Formatters.Register( new ActionResultFormatter() );
            LoggingServices.Formatters.Register( new ObjectResultFormatter() );



            // Execute the web app.
            using ( logger )
            {
                CreateWebHostBuilder( args ).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder( string[] args ) =>
            WebHost.CreateDefaultBuilder( args )
                .UseSerilog()
                .UseStartup<Startup>();




    }

}
