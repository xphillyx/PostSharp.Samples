using PostSharp.Patterns.Diagnostics.Audit;
using PostSharp.Patterns.Diagnostics.Backends.Audit;
using PostSharp.Patterns.Diagnostics.Contexts;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using PostSharp.Patterns.Formatters;

namespace PostSharp.Samples.Audit.Extended
{
    public class ExtendedAuditRecordBuilder : AuditRecordBuilder
    {
        public ExtendedAuditRecordBuilder(AuditBackend backend) : base(backend)
        {
        }

        protected override AuditRecord CreateRecord(LoggingContext context, ref LogRecordInfo recordInfo, ref LogMemberInfo memberInfo)
        {
            return new ExtendedAuditRecord(context.Source.SourceType, memberInfo.MemberName, recordInfo.RecordKind);
        }

        public override void SetParameter<T>(int index, string parameterName, ParameterDirection direction, string typeName, T value,
            IFormatter<T> formatter)
        {
            var businessObject = value as BusinessObject;

            if (businessObject != null)
            {
                ((ExtendedAuditRecord) this.CurrentRecord).RelatedBusinessObjects.Add(businessObject);
            }

            base.SetParameter(index, parameterName, direction, typeName, value, formatter);
        }
    }
}