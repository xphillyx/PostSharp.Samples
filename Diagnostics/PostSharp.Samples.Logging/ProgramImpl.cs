using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Audit;

namespace PostSharp.Samples.Logging
{
    static class ProgramImpl
    {
        static readonly Logger logger = Logger.GetLogger();


        public static void Execute()
        {
            Fibonacci(5);


            Task.WaitAll(
                ProgramImpl.ReadAndHashAsync("https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/pineisland_oli_2017026_lrg-crop.jpg"),
                ProgramImpl.ReadAndHashAsync("https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/pia20521-1041.jpg"));

            var activity = logger.OpenActivity("Hashing images without logging");
            try
            {
                logger.Write(LogLevel.Info, "Disabling debug logging the WebUtil.");
                LoggingServices.DefaultBackend.GetSource(LoggingRoles.Tracing, typeof(ProgramImpl)).SetLevel(LogLevel.Info);
                Task.WaitAll(
                    ProgramImpl.ReadAndHashAsync("https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/pineisland_oli_2017026_lrg-crop.jpg"),
                    ProgramImpl.ReadAndHashAsync("https://www.nasa.gov/sites/default/files/styles/full_width_feature/public/thumbnails/image/pia20521-1041.jpg"));

                activity.SetSuccess();
            }
            catch (Exception e)
            {
                activity.SetException(e);
                throw;
            }

            UpdateCustomer(5, new CustomerData { FirstName = "Joe", LastName = "Brick"});

        }

        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        [Audit]
        public static void UpdateCustomer(int customerId, CustomerData customerData)
        {
            logger.Write(LogLevel.Info, "Let's pretend we're updating customer {CustomerId} with {Data}.", customerId, customerData);
        }

        public static async Task ReadAndHashAsync(string url)
        {
            var hashAlgorithm = HashAlgorithm.Create("SHA256");
            hashAlgorithm.Initialize();

            using (var webClient = new WebClient())
            {
                var buffer = new byte[16 * 1024];

                logger.Write(LogLevel.Info, "Using a {BufferSize}-byte buffer.", buffer.Length);

                using (var stream = webClient.OpenRead(url))
                {
                    int countRead;
                    while ((countRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        logger.Write(LogLevel.Info, "Got {CountRead} bytes.", countRead);
                        hashAlgorithm.ComputeHash(buffer, 0, countRead);
                    }
                }
            }
        }
    }
}