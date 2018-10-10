using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientExample;
using PostSharp.Aspects;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Serialization;
using Serilog.Context;

[assembly: InstrumentOutgoingRequestsAspect( AttributeTargetAssemblies = "System.Net.Http", 
    AttributeTargetTypes = "System.Net.Http.HttpClient", 
    AttributeTargetMembers = "regex:(Get*|Delete|Post|Push|Patch)Async" )]

namespace ClientExample
{
    [PSerializable]
    internal class InstrumentOutgoingRequestsAspect : MethodInterceptionAspect
    {
        private static readonly Logger logger = Logger.GetLogger( LoggingRoles.Tracing, typeof( HttpClient ) );

       

        public override async Task OnInvokeAsync( MethodInterceptionArgs args )
        {
            HttpClient http = (HttpClient) args.Instance;

            string operationId = Guid.NewGuid().ToString();

            http.DefaultRequestHeaders.Remove( "Request-Id" );
            http.DefaultRequestHeaders.Add( "Request-Id", operationId.ToString() );

            string verb = Trim( args.Method.Name, "Async" );

            using ( LogContext.PushProperty( "OperationId", operationId ) )
            using ( LogActivity activity = logger.OpenActivity( "{Verb} {Url}", verb, args.Arguments[0] ) )
            {
                try
                {
                    Task t = base.OnInvokeAsync( args );

                    // We need to call Suspend/Resume because we're calling LogActivity from an aspect and 
                    // aspects are not automatically enhanced.
                    // In other code, this is done automatically.
                    if ( !t.IsCompleted )
                    {
                        activity.Suspend();
                        try
                        {
                            await t;
                        }
                        finally
                        {
                            activity.Resume();
                        }
                    }


                    HttpResponseMessage response = (HttpResponseMessage) args.ReturnValue;

                    if ( response.IsSuccessStatusCode )
                    {
                        activity.SetSuccess( "Success: {StatusCode}", response.StatusCode );
                    }
                    else
                    {
                        activity.SetFailure( "Failure: {StatusCode}", response.StatusCode );
                    }

                }
                catch ( Exception e )
                {
                    activity.SetException( e );
                    throw;
                }
                finally
                {
                    http.DefaultRequestHeaders.Remove( "Request-Id" );
                }
            }

        }

        private static string Trim( string s, string suffix )
        {
            if ( s.EndsWith( suffix ) )
            {
                return s.Substring( 0, s.Length - suffix.Length );
            }
            else
            {
                return s;
            }
        }
    }
}