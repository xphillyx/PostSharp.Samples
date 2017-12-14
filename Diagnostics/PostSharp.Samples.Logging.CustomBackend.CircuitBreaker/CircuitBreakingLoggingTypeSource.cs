using System;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  // Each back-end has its own derived type
  [Log(AttributeExclude = true)]
  public class CircuitBreakingLoggingTypeSource : NLogLoggingTypeSource
  {
    public CircuitBreakingLoggingTypeSource(LoggingNamespaceSource parent, Type type) : base(parent, type)
    {
    }

    protected override bool IsBackendEnabled(LogLevel level)
    {
      if (!LoggingCircuitBreaker.Closed)
        return false;

      try
      {
        return base.IsBackendEnabled(level);
      }
      catch (Exception)
      {
        LoggingCircuitBreaker.Break();
        return false;
      }
    }

    public override void Refresh()
    {
      base.Refresh();
    }
  }
}