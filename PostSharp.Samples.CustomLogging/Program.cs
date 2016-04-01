using System;
using PostSharp.Samples.CustomLogging.Aspects;

// Add logging to every method in the assembly.
[assembly: LogMethod(AttributePriority = 0)]

// Remove logging from the Aspects namespace to avoid infinite recursions (logging would log itself).
[assembly: LogMethod(AttributePriority = 1, AttributeExclude = true, AttributeTargetTypes = "PostSharp.Samples.CustomLogging.Aspects.*")]

// Add logging to System.Math to show we can add logging to anything.
[assembly: LogMethod(AttributePriority = 2, AttributeTargetAssemblies = "mscorlib", AttributeTargetTypes = "System.Math" )]

namespace PostSharp.Samples.CustomLogging
{
    static class Program
    {
        [LogSetValue]
        private static int Value;

        static void Main(string[] args)
        {
            // Demonstrate that we can create a nice hierarchical log including parameter and return values.
            Value = Fibonacci(5);

            // Demonstrate how exceptions are logged.
            try
            {
                Fibonacci(-1);
            }
            catch
            {
                
            }

            // Demonstrate that we can add logging to system methods, too.
            Console.WriteLine(Math.Sin(5));
        }


        private static int Fibonacci(int n)
        {
            if ( n < 0 )
                throw new ArgumentOutOfRangeException();
            else if (n == 0)
                return 0;
            else if (n == 1)
                return 1;
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}
