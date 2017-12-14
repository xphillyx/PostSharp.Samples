using System;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Diagnostics.Contexts;
using PostSharp.Patterns.Diagnostics.RecordBuilders;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  public class CircuitBreakingLoggingBackend : NLogLoggingBackend
  {
    protected override LoggingTypeSource CreateTypeSource(LoggingNamespaceSource parent, Type type)
    {
      LoggingTypeSource typeSource = new CircuitBreakingLoggingTypeSource(parent, type);
      LoggingCircuitBreaker.StatusChanged += (sender, args) => typeSource.Refresh();
      return typeSource;
    }

    public override LogRecordBuilder CreateRecordBuilder()
    {
      return new CircuitBreakingLogRecordBuilder(this);
    }

    [LoggingCircuitBreaker]
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
    }

    protected override ThreadLoggingContext CreateThreadContext()
    {
      return new CircuitBreakingThreadLoggingContext(this, CreateRecordBuilder());
    }

    protected override EphemeralLoggingContext CreateEphemeralContext(ThreadLoggingContext threadContext)
    {
      return new CircuitBreakingEphemeralLoggingContext(this, threadContext);
    }

    protected override AsyncMethodLoggingContext CreateAsyncMethodContext()
    {
      return new CircuitBreakingAsyncMethodLoggingContext(this);
    }

    protected override IteratorLoggingContext CreateIteratorContext()
    {
      return new CircuitBreakingIteratorLoggingContext(this);
    }

    protected override SyncMethodLoggingContext CreateSyncMethodContext(ThreadLoggingContext threadContext)
    {
      return new CircuitBreakingSyncMethodLoggingContext(threadContext);
    }

    protected override SyncCustomActivityLoggingContext CreateSyncCustomActivityContext(
      ThreadLoggingContext threadContext)
    {
      return new CircuitBreakingSyncCustomActivityLoggingContext(threadContext);
    }

    protected override AsyncCustomActivityLoggingContext CreateAsyncCustomActivityContext()
    {
      return new CircuitBreakingAsyncCustomActivityLoggingContext(this);
    }

    [LoggingCircuitBreaker]
    public override string ToString()
    {
      return base.ToString();
    }
  }
}