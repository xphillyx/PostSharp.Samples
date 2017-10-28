using System.Security.Principal;
using PostSharp.Patterns.Diagnostics.Audit;

namespace PostSharp.Samples.Audit
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure the auditing services.
            AuditServices.RecordPublished += OnAuditRecordPublished;

            // Simulate some audited business operation.
            var po = new PurchaseOrder();
            po.Approve();

        }

        private static void OnAuditRecordPublished(object sender, AuditRecordEventArgs e)
        {
            var record = new AuditRecord(
                WindowsIdentity.GetCurrent().Name,
                (BusinessObject) e.Record.Target,
                e.Record.MemberName,
                e.Record.Text
                );

            record.AppendToDatabase();
        }
    }
}
