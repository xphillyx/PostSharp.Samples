using System;
using System.Resources;

namespace PostSharp.Samples.ValidateResourceString
{
    class Program
    {
        private const string resourceName = "PostSharp.Samples.ValidateResourceString.MyResource";
        static readonly ResourceManager resourceManager = new ResourceManager(resourceName, typeof(Program).Assembly);

        static void Main(string[] args)
        {
            Console.WriteLine(GetResourceString("String1"));
            Console.WriteLine(GetResourceString("String2"));
            Console.WriteLine(GetResourceString("Strong3"));
        }

        public static string GetResourceString([ValidateResourceString(resourceName)] string key)
        {
            return resourceManager.GetString(key);

        }
    }
}
