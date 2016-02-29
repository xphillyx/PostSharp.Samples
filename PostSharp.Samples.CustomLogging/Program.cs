using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharp.Samples.CustomLogging
{
    [LogMethod]
    class Program
    {
        [LogSetValue]
        private static int Value;

        static void Main(string[] args)
        {

            Value = Fibonacci(5);

            try
            {
                Fibonacci(-1);
            }
            catch
            {
                
            }
        }


        public static int Fibonacci(int n)
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
