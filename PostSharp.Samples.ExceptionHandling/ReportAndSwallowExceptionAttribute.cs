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
    class ReportAndSwallowExceptionAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            // Write the default exception information.
            Console.WriteLine("Exception information");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine(args.Exception.ToString());
            Console.WriteLine("--------------------------------------------------------------");
            StringBuilder additionalContext = (StringBuilder) args.Exception.Data["Context"];

            // Write the additional information that was gathered by AddContextOnExceptionAttribute.
            if (additionalContext != null)
            {
                Console.WriteLine("Additional context information (call stack with parameter values)");
                Console.WriteLine("--------------------------------------------------------------");
                Console.Write(additionalContext.ToString());
                Console.WriteLine("--------------------------------------------------------------");
            }

            // Ignore the exception.
            Console.WriteLine("*** Ignoring the exception ***");
            args.FlowBehavior = FlowBehavior.Continue;
            
        }
    }
}
