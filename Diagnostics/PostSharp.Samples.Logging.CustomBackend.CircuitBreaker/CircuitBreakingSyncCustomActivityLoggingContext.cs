using System.Text;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
    [Log(AttributeExclude = true)]
    [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
    public class CircuitBreakingSyncCustomActivityLoggingContext : SyncCustomActivityLoggingContext
    {
        public CircuitBreakingSyncCustomActivityLoggingContext(ThreadLoggingContext threadContext) : base(threadContext)
        {
        }

        protected override void ReturnToPool()
        {
            base.ReturnToPool();
        }

        public override void Open(LoggingTypeSource typeSource, LogActivityOptions options)
        {
            base.Open(typeSource, options);
        }

        public override void SetWaitDependency(object waited)
        {
            base.SetWaitDependency(waited);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);
        }

        public override CorrelationCookie CreateCorrelationCookie()
        {
            return base.CreateCorrelationCookie();
        }

        public override void SetCorrelation(CorrelationCookie correlationCookie)
        {
            base.SetCorrelation(correlationCookie);
        }
    }
}
