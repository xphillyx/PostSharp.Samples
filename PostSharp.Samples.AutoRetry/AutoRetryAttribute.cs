using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Samples.AutoRetry
{
    [PSerializable]
    [LinesOfCodeAvoided(5)]
    public sealed class AutoRetryAttribute : MethodInterceptionAspect
    {
        public AutoRetryAttribute()
        {
            this.MaxRetries = 5;
            this.Delay = 3;
            this.HandledExceptions = new [] { typeof(WebException), typeof(DataException) };
        }

        public int MaxRetries { get; set; }

        public float Delay { get; set; }

        public Type[] HandledExceptions { get; set; }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            for (var i = 0; ; i++)
            {
                try
                {
                    args.Proceed();

                    // If we get here, it means the execution was successful.
                    return;
                }
                catch (Exception e)
                {
                    if (i < this.MaxRetries && (this.HandledExceptions == null || this.HandledExceptions.Any(type => type.IsInstanceOfType(e))))
                    {
                        Console.WriteLine("Method failed with exception {0}. Sleeping {1} s and retrying. This was our {2}th attempt.",
                            e.GetType().Namespace, this.Delay, i+1);
                        Thread.Sleep(TimeSpan.FromSeconds(this.Delay));

                        // Continue to the next iteration.
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}
