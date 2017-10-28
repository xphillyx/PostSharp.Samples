using PostSharp.Patterns.Diagnostics.Audit;

namespace PostSharp.Samples.Audit
{
    public class PurchaseOrder : BusinessObject
    {
        [Audit]
        public void Approve(string comment = null)
        {
            
        }
    }
}