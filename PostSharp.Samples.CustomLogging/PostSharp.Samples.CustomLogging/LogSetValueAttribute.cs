using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Samples.CustomLogging
{
    [PSerializable]
    public sealed class LogSetValueAttribute : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Setting ");
            Formatter.AppendTypeName(stringBuilder, args.Location.DeclaringType);
            if (args.Index.Count != 0)
            {
                Formatter.AppendArguments(stringBuilder, args.Index);
            }
            stringBuilder.Append(" = ");
            stringBuilder.Append(args.Value);

            Logger.WriteLine(stringBuilder.ToString());

            base.OnSetValue(args);
        }
    }
}
