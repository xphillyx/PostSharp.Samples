using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Samples.CustomCaching
{
    [PSerializable]
    public sealed class CacheAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var stringBuilder = new StringBuilder();
            AppendCallInformation(args, stringBuilder);

            var cacheKey = stringBuilder.ToString();

            var cachedValue = MemoryCache.Default.Get(cacheKey);

            if (cachedValue != null)
            {
                // If the value is already in the cache, don't even execute the method. Set the return value from the cache and return immediately.
                args.ReturnValue = cachedValue;
                args.FlowBehavior = FlowBehavior.Return;
            }
            else
            {
                // If the value is not in the cache, continue with method execution, but store the cache key so we can reuse it when the method exits.
                args.MethodExecutionTag = cacheKey;
                args.FlowBehavior = FlowBehavior.Continue;
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var cacheKey = (string) args.MethodExecutionTag;
            MemoryCache.Default[cacheKey] = args.ReturnValue;
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
