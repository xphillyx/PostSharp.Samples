using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Samples.ExceptionHandling;

// Add the AddContextOnException aspect to all methods in the assembly.
[assembly:AddContextOnException]

namespace PostSharp.Samples.ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            MainCore();
            Console.WriteLine("The program returns successfully.");
        }

        [ReportAndSwallowException]
        private static void MainCore()
        {
            Fibonacci(5);
        }

        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException();
            else if (n == 0)
                return 0;
            // Intentionally commented out to cause an exception.
            // else if (n == 1)
            //    return 1;
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}
