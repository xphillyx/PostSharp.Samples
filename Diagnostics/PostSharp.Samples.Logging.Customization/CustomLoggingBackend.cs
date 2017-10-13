using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using PostSharp.Patterns.Formatters;
using System;
using System.Threading;

namespace PostSharp.Samples.Logging
{
    [Log(AttributeExclude = true)]
    class CustomLoggingBackend : ConsoleLoggingBackend
    {
        public override LogRecordBuilder CreateRecordBuilder()
        {
            return new RecordBuilder(this);
        }


        [Log(AttributeExclude = true)]
        class RecordBuilder : ConsoleLogRecordBuilder
        {
            public RecordBuilder(ConsoleLoggingBackend backend) : base(backend)
            {
            }

            public override void SetParameter<T>(int index, string parameterName, ParameterDirection direction, string typeName, T value,
                IFormatter<T> formatter)
            {
                this.StringBuilder.Append('[');
                this.StringBuilder.Append(direction.ToString());
                this.StringBuilder.Append(']', ' ');

                base.SetParameter(index, parameterName, direction, typeName, value, formatter);
            }

            protected override void Write(UnsafeString message)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} \u00A6 " + message);
            }
        }
    }
}
