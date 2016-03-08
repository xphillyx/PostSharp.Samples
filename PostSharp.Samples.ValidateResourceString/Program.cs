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
            // These two method calls are valid.
            Console.WriteLine(GetResourceString("String1"));
            Console.WriteLine(GetResourceString("String2"));

            // There is a warning for the following line because Strong3 is not a valid string name.
            Console.WriteLine(GetResourceString("Strong3"));
        }

        private static string GetResourceString([ValidateResourceString(resourceName)] string key)
        {
            return resourceManager.GetString(key) ?? "*** Error ***";

        }
    }
}
