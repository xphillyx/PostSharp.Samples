using System;
using PostSharp.Patterns.Diagnostics;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
  [Log(AttributeExclude = true)]
  public static class LoggingCircuitBreaker
  {
    public static bool Closed { get; private set; } = true;


    public static void Break()
    {
      Closed = false;
      StatusChanged?.Invoke(null, EventArgs.Empty);
    }

    public static void Reset()
    {
      Closed = true;
      StatusChanged?.Invoke(null, EventArgs.Empty);
    }

    public static event EventHandler StatusChanged;
    
  }
}