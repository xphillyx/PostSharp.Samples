using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Samples.ExceptionHandling
{
    [PSerializable]
    public sealed class AddContextOnExceptionAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            // Get or create a StringBuilder for the exception where we will add additional context data.
            StringBuilder stringBuilder = (StringBuilder) args.Exception.Data["Context"];
            if (stringBuilder == null)
            {
                stringBuilder = new StringBuilder();
                args.Exception.Data["Context"] = stringBuilder;

            }


            AppendCallInformation(args, stringBuilder);
            stringBuilder.AppendLine();
        }

        private static void AppendCallInformation(MethodExecutionArgs args, StringBuilder stringBuilder)
        {
            var declaringType = args.Method.DeclaringType;
            Formatter.AppendTypeName(stringBuilder, declaringType);
            stringBuilder.Append('.');
            stringBuilder.Append(args.Method.Name);

            if (args.Method.IsGenericMethod)
            {
                var genericArguments = args.Method.GetGenericArguments();
                Formatter.AppendGenericArguments(stringBuilder, genericArguments);
            }

            var arguments = args.Arguments;

            Formatter.AppendArguments(stringBuilder, arguments);
        }
    }
}
