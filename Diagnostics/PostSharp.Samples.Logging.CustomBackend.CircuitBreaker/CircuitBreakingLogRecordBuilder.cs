using System;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Diagnostics.Contexts;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using PostSharp.Patterns.Formatters;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
    // Each back-end has its own derived type
    [Log(AttributeExclude = true)]
    [LoggingCircuitBreaker(AttributeTargetElements = MulticastTargets.Method)]
    public class CircuitBreakingLogRecordBuilder : NLogLogRecordBuilder 
    {
        
        public CircuitBreakingLogRecordBuilder(NLogLoggingBackend backend) : base(backend)
        {
        }

        
        public override void BeginRecord(LoggingContext context, ref LogRecordInfo recordInfo, ref LogMemberInfo memberInfo)
        {
            base.BeginRecord(context, ref recordInfo, ref memberInfo);
        }

        public override void BeginCustomRecord(LoggingContext context, ref CustomLogRecordInfo recordInfo)
        {
            base.BeginCustomRecord(context, ref recordInfo);
        }

        public override void SetThis<T>(T value, IFormatter<T> formatter)
        {
            base.SetThis(value, formatter);
        }

        public override void SetException(Exception exception)
        {
            base.SetException(exception);
        }

        public override void SetExecutionTime(double executionTime, bool isOvertime)
        {
            base.SetExecutionTime(executionTime, isOvertime);
        }

        public override void SetParameter<T>(int index, string parameterName, ParameterDirection direction, string typeName, T value,
            IFormatter<T> formatter)
        {
            base.SetParameter(index, parameterName, direction, typeName, value, formatter);
        }

        public override void SetReturnValue<T>(int index, string returnValueName, string typeName, T value, IFormatter<T> formatter)
        {
            base.SetReturnValue(index, returnValueName, typeName, value, formatter);
        }

        public override void WriteCustomParameter<T>(int index, ArraySegment<char> parameterName, T value, IFormatter<T> formatter)
        {
            base.WriteCustomParameter(index, parameterName, value, formatter);
        }

        public override void WriteCustomString(ArraySegment<char> str)
        {
            base.WriteCustomString(str);
        }
    
        public override string ToString()
        {
            return base.ToString();
        }

        public override void SetTypeGenericParameter<T>(int index)
        {
            base.SetTypeGenericParameter<T>(index);
        }

        public override void SetMethodGenericParameter<T>(int index)
        {
            base.SetMethodGenericParameter<T>(index);
        }
    }
}
