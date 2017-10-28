using System;
using PostSharp.Patterns.Diagnostics.Backends.Audit;
using PostSharp.Patterns.Diagnostics.Contexts;
using PostSharp.Patterns.Diagnostics.RecordBuilders;

namespace PostSharp.Samples.Audit
{
    public class DbAuditRecord
    {
        public DbAuditRecord(string user, BusinessObject businessObject, string method, string description)
        {
            User = user;
            BusinessObject = businessObject;
            Method = method;
            Description = description;
        }

        public BusinessObject BusinessObject { get; }
        public string Method { get; }
        public string User { get; }
        public string Description { get; }
        public DateTimeOffset Time { get; } = DateTimeOffset.Now;

        public void AppendToDatabase()
        {
            Console.WriteLine($"TODO - Write to the database: {{BusinessObjectId={this.BusinessObject.Id}, Operation={this.Method}, Description=\"{this.Description}\", User={this.User}}}.");
        }
    }

}