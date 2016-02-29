using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PostSharp.Samples.AutoRetry
{
    class Program
    {
        static readonly Random random = new Random();
        static readonly Stopwatch stopwatch = Stopwatch.StartNew();

        static void Main(string[] args)
        {
            Console.WriteLine(DownloadFile());
        }

        [AutoRetry(MaxRetries = 3)]
        private static string DownloadFile()
        {
            WriteMessage("Attempting to download the file.");

            if (random.NextDouble() < 0.8)
            {
                WriteMessage("Network failure.");
                throw new WebException();
            }
            else
            {
                WriteMessage("Success!");
                return "Hello, world.";
            }
        }

        static void WriteMessage(string message)
        {
            Console.WriteLine("{0} ms - {1}", stopwatch.ElapsedMilliseconds, message);
        }
    }
}
