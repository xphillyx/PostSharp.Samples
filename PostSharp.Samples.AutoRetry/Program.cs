using System;
using System.Diagnostics;
using System.Net;

namespace PostSharp.Samples.AutoRetry
{
    internal static class Program
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

            // Randomly decide if the method call should succeed or fail.
            if (random.NextDouble() < 0.8)
            {
                // Simulate a network failure.

                WriteMessage("Network failure.");
                throw new WebException();
            }
            else
            {
                // Simulate success.
                WriteMessage("Success!");
                return "Hello, world.";
            }
        }

        // Writes a message to the console with a timestamp.
        private static void WriteMessage(string message)
        {
            Console.WriteLine("{0} ms - {1}", stopwatch.ElapsedMilliseconds, message);
        }
    }
}
