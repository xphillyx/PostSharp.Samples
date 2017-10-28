using System;
using PostSharp.Patterns.Diagnostics;
using ServiceStack.Logging;

namespace PostSharp.Samples.Logging.CustomBackend.ServiceStack
{
    public class ServiceStackLoggingTypeSource : LoggingTypeSource
    {
        public ILog Log { get; }

        public ServiceStackLoggingTypeSource(LoggingNamespaceSource parent, Type sourceType) : base(parent, sourceType)
        {
            this.Log = LogManager.GetLogger(sourceType);
        }

        protected override bool IsBackendEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return this.Log.IsDebugEnabled;

                default:
                    return true;
            }
        }
    }
}