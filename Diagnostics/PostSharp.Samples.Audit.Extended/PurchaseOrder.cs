using PostSharp.Patterns.Diagnostics.Audit;

namespace PostSharp.Samples.Audit.Extended
{
    public class PurchaseOrder : BusinessObject
    {
        [Audit]
        public void AssignTo(Employee employee)
        {
            
        }
    }
}