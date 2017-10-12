using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Audit;
using System;

namespace PostSharp.Samples.Logging
{
    [Log(AttributeExclude = true)]
    static class Program
    {
        
        static void Main(string[] args)
        {
            var backend = new Patterns.Diagnostics.Backends.Console.ConsoleLoggingBackend();
            backend.Options.Delimiter = " \u00A6 ";
            LoggingServices.DefaultBackend = backend ;

            LoggingServices.Formatters.Register(new FancyIntFormatter());
            AuditServices.RecordPublished += OnAuditRecordPublished;

            ProgramImpl.Execute();


        }

        private static void OnAuditRecordPublished(object sender, AuditRecordEventArgs e)
        {
            // Typically, you would write this into a database.
            Console.WriteLine("AUDIT: " + e.Record.Text);
        }

    }
}
