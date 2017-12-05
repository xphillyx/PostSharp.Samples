using System.Text;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
    [Log(AttributeExclude = true)]
    [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
    public class CircuitBreakingAsyncCustomActivityLoggingContext : AsyncCustomActivityLoggingContext
    {
        public CircuitBreakingAsyncCustomActivityLoggingContext(LoggingBackend backend) : base(backend)
        {
        }

        public override void Open(LoggingTypeSource typeSource, LogActivityOptions options)
        {
            base.Open(typeSource, options);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void SetWaitDependency(object waited)
        {
            base.SetWaitDependency(waited);
        }

        protected override void OnResume(AsyncLoggingContext resumedContext)
        {
            base.OnResume(resumedContext);
        }

        protected override void OnSuspend(AsyncLoggingContext suspendedContext)
        {
            base.OnSuspend(suspendedContext);
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
