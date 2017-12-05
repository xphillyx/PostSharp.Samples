using System.Text;
using System.Threading.Tasks;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
    [Log(AttributeExclude = true)]
    [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
    public class CircuitBreakingAsyncMethodLoggingContext : AsyncMethodLoggingContext
    {
        public CircuitBreakingAsyncMethodLoggingContext(LoggingBackend backend) : base(backend)
        {
        }

        public override void Open(Task task, ref LogMemberInfo memberInfo)
        {
            base.Open(task, ref memberInfo);
        }

        protected override void OnResume(AsyncLoggingContext resumedContext)
        {
            base.OnResume(resumedContext);
        }

        protected override void OnSuspend(AsyncLoggingContext suspendedContext)
        {
            base.OnSuspend(suspendedContext);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);
        }

        public override void SetWaitDependency(object waited)
        {
            base.SetWaitDependency(waited);
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
