using Microsoft.AspNetCore.Http;
using PostSharp.Patterns.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;

[assembly: Log]

namespace MicroserviceExample
{
    [Log( AttributeExclude = true )]
    public class RequestIdEnricher : ILogEventEnricher
    {
        public static IHttpContextAccessor HttpContextAccessor { get; set; }
        public void Enrich( LogEvent logEvent, ILogEventPropertyFactory propertyFactory )
        {
            if ( HttpContextAccessor == null )
            {
                return;
            }

            string requestId = HttpContextAccessor.HttpContext.Request.Headers["Request-Id"];


            if ( requestId != null )
            {
                logEvent.AddPropertyIfAbsent( propertyFactory.CreateProperty( "RequestId", requestId ) );
            }
        }
    }

}
