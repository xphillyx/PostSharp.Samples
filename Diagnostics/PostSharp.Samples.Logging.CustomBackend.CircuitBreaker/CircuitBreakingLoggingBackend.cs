using System;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Diagnostics.Contexts;
using PostSharp.Patterns.Diagnostics.RecordBuilders;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
    [Log(AttributeExclude = true)]
    public class CircuitBreakingLoggingBackend : NLogLoggingBackend // ConsoleLoggingBackend
    {
        protected override LoggingTypeSource CreateTypeSource(LoggingNamespaceSource parent, Type type)
        {
            LoggingTypeSource typeSource = new CircuitBreakingLoggingTypeSource(parent, type);
            // #15691
            //LoggingCircuitBreaker.Broken += (sender, args) => typeSource.Refresh();
            LoggingCircuitBreaker.Broken += (sender, args) => typeSource.SetLevel(LogLevel.None);
            return typeSource;
        }

        public override LogRecordBuilder CreateRecordBuilder()
        {
            return new CircuitBreakingLogRecordBuilder(this);
        }

        protected override TextLoggingBackendOptions GetTextBackendOptions()
        {
            return base.GetTextBackendOptions();
        }

        [LoggingCircuitBreaker]
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override ThreadLoggingContext CreateThreadContext()
        {
            return new CircuitBreakingThreadLoggingContext(this, this.CreateRecordBuilder());
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

        protected override SyncCustomActivityLoggingContext CreateSyncCustomActivityContext(ThreadLoggingContext threadContext)
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

      public void OnException(Exception exception)
      {
        IsBroken = true;
      }

      public bool IsBroken { get; private set; }
    }
}
