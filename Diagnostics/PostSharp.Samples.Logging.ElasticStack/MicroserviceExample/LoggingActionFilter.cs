using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using PostSharp.Patterns.Diagnostics;

namespace MicroserviceExample
{
    [Log(AttributeExclude = true)]
    public class LoggingActionFilter :  IAsyncActionFilter
    {
        static readonly Random random = new Random(0); // Pseudo-random to make demos more predictable.
        static readonly Logger logger = Logger.GetLogger();
        public static LoggingContextConfiguration DetailedLoggingConfiguration;

        private static readonly ILoggingProperty<string> parentOperationIdProperty;

        static LoggingActionFilter()
        {
            parentOperationIdProperty = logger.GetOrCreateProperty<string>( "ParentOperationId" );
        }

        public async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next )
        {

            if ( MustLog() )
            {
                FluentLogger fluentLogger;


                // Read the Request-Id header so we can assign it to the activity.
                string parentOperationId = context.HttpContext.Request.Headers["Request-Id"];

                if ( !string.IsNullOrEmpty( parentOperationId ) )
                {
                    fluentLogger = logger.WithProperty( parentOperationIdProperty, parentOperationId );
                }
                else
                {
                    fluentLogger = logger;
                }

               // Enable logging.
                using ( LoggingServices.DefaultBackend.WithContextConfiguration( DetailedLoggingConfiguration ) )
                {
                    LogActivity activity = fluentLogger.OpenAsyncActivity( "Processing {Verb} {Path}", context.HttpContext.Request.Method, context.HttpContext.Request.Path );
                    try
                    {
                        Task task = next();
                        await task;

                        if ( task is Task<ActionExecutedContext> typedTask )
                        {
                            if ( typedTask.Result.Exception != null )
                            {
                                activity.SetException(typedTask.Result.Exception );
                            }
                            else
                            {
                                activity.SetSuccess("Returning {Result}", typedTask.Result.Result );
                            }
                        }
                        else
                        {
                            activity.SetSuccess();
                        }

                        
                    }
                    catch ( Exception e )
                    {
                        activity.SetException( e );
                        throw;
                    }
                }
            }
            else
            {

                // TODO: Context property will not be passed if verbosity is low. This is a PostSharp limitation.

                Console.WriteLine( $"Request {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} will not be logged." );

                await next();
            }

        }

        private static bool MustLog()
        {
            lock ( random )
            {
                return random.NextDouble() > 0.5;
            }
        }
    }
}
