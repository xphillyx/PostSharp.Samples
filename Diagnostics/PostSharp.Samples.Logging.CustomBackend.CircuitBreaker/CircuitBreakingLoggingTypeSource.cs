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

        private bool refreshedAfterCircuitBreak;

        protected override bool IsBackendEnabled(LogLevel level)
        {
            if (!LoggingCircuitBreaker.Closed)
                return false;

            return base.IsBackendEnabled(level);
        }

        public override void Refresh()
        {
            base.Refresh();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
