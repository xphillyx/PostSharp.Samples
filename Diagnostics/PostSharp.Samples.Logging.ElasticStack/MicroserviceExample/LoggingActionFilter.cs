using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using PostSharp.Patterns.Diagnostics;
using Serilog.Context;

namespace MicroserviceExample
{
    [Log( AttributeExclude = true )]
    public class LoggingActionFilter : IAsyncActionFilter
    {
        static readonly Logger logger = Logger.GetLogger();

        public async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next )
        {

            // Read the Request-Id header so we can assign it to the activity.
            string parentOperationId = context.HttpContext.Request.Headers["Request-Id"];

            if ( !string.IsNullOrEmpty( parentOperationId ) )
            {
                using ( LogContext.PushProperty( "ParentOperationId", parentOperationId ) )
                {
                    await next();
                }
            }
            else
            {
                await next();
            }



        }
    }
}
