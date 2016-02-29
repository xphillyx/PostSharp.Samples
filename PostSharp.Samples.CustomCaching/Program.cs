using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharp.Samples.CustomCaching
{
    class Program
    {
        static readonly Stopwatch stopwatch = Stopwatch.StartNew();

        static void Main(string[] args)
        {
            WriteMessage(Hello("world"));
            WriteMessage(Hello("universe"));
            WriteMessage(Hello("world"));
            WriteMessage(Hello("universe"));
        }

        static void WriteMessage(string message)
        {
            Console.WriteLine("{0} ms - {1}", stopwatch.ElapsedMilliseconds, message);
        }

        [Cache]
        private static string Hello(string who)
        {
            WriteMessage( string.Format("Doing complex stuff for {0}.", who));
            Thread.Sleep(500);
            return string.Format("Hello, {0}", who);
        }
    }
}
