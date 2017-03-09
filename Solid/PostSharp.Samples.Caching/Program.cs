using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Caching;
using System;
using System.Threading;
using PostSharp.Patterns.Caching.Backends.Redis;
using StackExchange.Redis;
using System.Linq;

namespace PostSharp.Samples.Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RedisServer.Start())
            {
                using (ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost:6380,abortConnect = False"))
                {
                    connection.ErrorMessage += (sender, eventArgs) => Console.Error.WriteLine(eventArgs.Message);
                    connection.ConnectionFailed += (sender, eventArgs) => Console.Error.WriteLine(eventArgs.Exception);

                    using (RedisCachingBackend backend = RedisCachingBackend.Create(connection))
                    using (RedisCacheDependencyOnlineCollector.Create(connection))  // With Redis, we need at least one instance of the collection engine.
                    {
                        CachingServices.Backend = backend;

                        // Testing direct invalidation.
                        Console.WriteLine("Retrieving the customer for the 1st time should hit the database.");
                        GetCustomer(1);
                        Console.WriteLine("Retrieving the customer for the 2nd time should NOT hit the database.");
                        GetCustomer(1);
                        Console.WriteLine("This should invalidate the GetCustomer method.");
                        UpdateCustomer(1, "New name");
                        Console.WriteLine("This should hit the database again because GetCustomer has been invalidated.");
                        GetCustomer(1);

                        // Testing indirect invalidation (dependencies).
                        Console.WriteLine("Retrieving the account list for the 1st time should hit the database.");
                        GetAccountsOfCustomer(1);
                        Console.WriteLine("Retrieving the account list for the 2nt time should NOT hit the database.");
                        var accounts = GetAccountsOfCustomer(1);
                        Console.WriteLine("This should invalidate the accounts");
                        UpdateAccount(accounts.First());
                        Console.WriteLine("This should hit the database again because GetAccountsOfCustomer has been invalidated.");
                        GetAccountsOfCustomer(1);

                        Console.WriteLine("Done!");
                    }
                }
            }
        }

        // Demonstrates simple caching.
        [Cache]
        public static Customer GetCustomer(int id)
        {
            Console.WriteLine($">> Retrieving the customer {id} from database...");
            Thread.Sleep(1000);
            return new Customer { Id = id, Name = "Rubber Debugging Duck" };
        }


        // Demonstrates declarative direct invalidation.
        [InvalidateCache(nameof(GetCustomer))]
        public static void UpdateCustomer(int id, string newName)
        {
            Console.WriteLine($">> Updating the customer {id} in database...");
            Thread.Sleep(1000);
        }


        // Demonstrates programmatic direct invalidation.
        public static void DeleteCustomer(int id, string newName)
        {
            Console.WriteLine($">> Deleting the customer {id} from database...");
            Thread.Sleep(1000);

            CachingServices.Invalidation.Invalidate(GetCustomer, id);
        }

        // Demonstrates object-oriented dependencies.
        [Cache]
        public static Account GetAccount(int id)
        {
            Console.WriteLine($">> Retrieving the account {id} from database...");
            Thread.Sleep(1000);

            var account = new Account { AccountId = id };

            CachingContext.Current.AddDependency(account);

            return account;
        }

        // Demonstrates nested dependencies.
        [Cache]
        public static IEnumerable<Account> GetAccountsOfCustomer(int customerId)
        {
            yield return GetAccount(1);
            yield return GetAccount(2);
        }

        // Demonstrates invaliding dependencies.
        public static void UpdateAccount(Account account)
        {
            Console.WriteLine($">> Updating the account {account.AccountId} in database...");
            Thread.Sleep(1000);
            CachingServices.Invalidation.Invalidate(account);
        }



    }

}
