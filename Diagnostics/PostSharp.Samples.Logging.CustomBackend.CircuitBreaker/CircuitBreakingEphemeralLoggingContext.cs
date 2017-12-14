using System.Text;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
  public class CircuitBreakingEphemeralLoggingContext : EphemeralLoggingContext
  {
    public CircuitBreakingEphemeralLoggingContext(LoggingBackend backend, ThreadLoggingContext threadContext) : base(
      backend, threadContext)
    {
    }

    public override void Open(LoggingContext parent, LoggingTypeSource typeSource)
    {
      base.Open(parent, typeSource);
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