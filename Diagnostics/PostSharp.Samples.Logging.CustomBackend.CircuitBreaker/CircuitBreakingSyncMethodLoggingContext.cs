using System.Text;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
  public class CircuitBreakingSyncMethodLoggingContext : SyncMethodLoggingContext
  {
    public CircuitBreakingSyncMethodLoggingContext(ThreadLoggingContext threadContext) : base(threadContext)
    {
    }

    public override LoggingContext ParentContext => base.ParentContext;

    protected override void ReturnToPool()
    {
      base.ReturnToPool();
    }

    public override void Open(ref LogMemberInfo memberInfo)
    {
      base.Open(ref memberInfo);
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