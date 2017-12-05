using System;

namespace PostSharp.Samples.Logging.CustomBackend.CircuitBreaker
{
    public static class LoggingCircuitBreaker
    {
        public static bool Closed { get; private set; } = true;


        public static void Break()
        {
            Closed = false;
            Broken?.Invoke(null, EventArgs.Empty);
        }

      public static event EventHandler Broken;
    }
}
