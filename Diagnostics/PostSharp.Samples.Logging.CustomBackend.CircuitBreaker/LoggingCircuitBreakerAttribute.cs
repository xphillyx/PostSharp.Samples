using System;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Serialization;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [PSerializable]
  public class LoggingCircuitBreakerAttribute : MethodLevelAspect
  {
    [OnMethodEntryAdvice, SelfPointcut]
    public void OnEntry([FlowBehavior] out FlowBehavior flowBehavior)
    {
      if (!LoggingCircuitBreaker.Closed)
      {
        flowBehavior = FlowBehavior.Return;
      }
      else
      {
        flowBehavior = FlowBehavior.Default;
      }
    }

    [OnMethodExceptionAdvice, SelfPointcut]
    public void OnException()
    {
      LoggingCircuitBreaker.Break();
    }
    
  }
}
