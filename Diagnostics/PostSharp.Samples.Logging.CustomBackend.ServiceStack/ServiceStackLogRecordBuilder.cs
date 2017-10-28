using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using PostSharp.Patterns.Formatters;

namespace PostSharp.Samples.Logging.CustomBackend.ServiceStack
{
    public class ServiceStackLogRecordBuilder : TextLogRecordBuilder
    {
        public ServiceStackLogRecordBuilder(TextLoggingBackend backend) : base(backend)
        {
        }

        protected override void Write(UnsafeString message)
        {
            var log = ((ServiceStackLoggingTypeSource) this.TypeSource).Log;
            var messageString = message.ToString();

            switch (this.Level)
            {
                case LogLevel.None:
                    break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    if (this.Exception == null)
                    {
                        log.Debug(messageString);
                    }
                    else
                    {
                        log.Debug(messageString, this.Exception);
                    }
                    break;

                case LogLevel.Info:
                    if (this.Exception == null)
                    {
                        log.Info(messageString);
                    }
                    else
                    {
                        log.Info(messageString, this.Exception);
                    }
                    break;

                case LogLevel.Warning:
                    if (this.Exception == null)
                    {
                        log.Warn(messageString);
                    }
                    else
                    {
                        log.Warn(messageString, this.Exception);
                    }
                    break;

                case LogLevel.Error:
                    if (this.Exception == null)
                    {
                        log.Error(messageString);
                    }
                    else
                    {
                        log.Error(messageString, this.Exception);
                    }
                    break;

                case LogLevel.Critical:
                    if (this.Exception == null)
                    {
                        log.Fatal(messageString);
                    }
                    else
                    {
                        log.Fatal(messageString, this.Exception);
                    }
                    break;
            }
            
        }
    }
}