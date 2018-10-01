using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using PostSharp.Patterns.Diagnostics;

namespace ClientExample
{
    [Log(AttributeExclude = true)]
    class HttpClientConfigurer : IObserver<DiagnosticListener>, IObserver<KeyValuePair<string, object>>
    {
        private static HttpClientConfigurer instance = new HttpClientConfigurer();
        static IDisposable subscription;
        public static void Configure()
        {
            if ( subscription == null )
            {
                subscription = DiagnosticListener.AllListeners.Subscribe( instance );
            }
        }


        void IObserver<DiagnosticListener>.OnNext( DiagnosticListener listener )
        {
            if ( listener.Name == "HttpHandlerDiagnosticListener" )
            {
                listener.Subscribe( this );
            }
        }

        void IObserver<KeyValuePair<string, object>>.OnNext( KeyValuePair<string, object> value )
        {
            
         }

        void IObserver<DiagnosticListener>.OnCompleted() { }

        void IObserver<KeyValuePair<string, object>>.OnCompleted() { }

        void IObserver<DiagnosticListener>.OnError( Exception error ) { }

        void IObserver<KeyValuePair<string, object>>.OnError( Exception error ) { }

        
    }
}
