using System.Collections;
using System.Text;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
  public class CircuitBreakingIteratorLoggingContext : IteratorLoggingContext
  {
    public CircuitBreakingIteratorLoggingContext(LoggingBackend backend) : base(backend)
    {
    }

    public override void Open(IEnumerator iterator, ref LogMemberInfo memberInfo)
    {
      base.Open(iterator, ref memberInfo);
    }

    public override void Resume()
    {
      base.Resume();
    }

    public override void Suspend()
    {
      base.Suspend();
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