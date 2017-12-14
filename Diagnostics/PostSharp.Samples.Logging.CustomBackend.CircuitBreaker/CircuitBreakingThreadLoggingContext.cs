using System.Text;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Contexts;
using PostSharp.Patterns.Diagnostics.RecordBuilders;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
  public class CircuitBreakingThreadLoggingContext : ThreadLoggingContext
  {
    public CircuitBreakingThreadLoggingContext(LoggingBackend backend, LogRecordBuilder recordBuilder) : base(backend,
      recordBuilder)
    {
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