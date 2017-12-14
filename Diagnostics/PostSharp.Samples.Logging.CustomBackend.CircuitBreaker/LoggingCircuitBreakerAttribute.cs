using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Serialization;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [PSerializable]
  public class LoggingCircuitBreakerAttribute : MethodLevelAspect
  {
    [OnMethodEntryAdvice]
    [SelfPointcut]
    public void OnEntry([FlowBehavior] out FlowBehavior flowBehavior)
    {
      if (!LoggingCircuitBreaker.Closed)
        flowBehavior = FlowBehavior.Return;
      else
        flowBehavior = FlowBehavior.Default;
    }

    [OnMethodExceptionAdvice(Master = nameof(OnEntry))]
    public void OnException([FlowBehavior] out FlowBehavior flowBehavior)
    {
      LoggingCircuitBreaker.Break();
      flowBehavior = FlowBehavior.Return;
    }
  }
}