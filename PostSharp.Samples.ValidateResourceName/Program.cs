using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PostSharp.Samples.ValidateResourceName
{
    class Program
    {
        private const string resourceName = "PostSharp.Samples.ValidateResourceName.MyResource";
        static readonly ResourceManager resourceManager = new ResourceManager(resourceName, typeof(Program).Assembly);

        static void Main(string[] args)
        {
            Console.WriteLine(GetResourceString("String1"));
            Console.WriteLine(GetResourceString("String2"));
            Console.WriteLine(GetResourceString("Strong3"));
        }

        public static string GetResourceString([ValidateResourceName(resourceName)] string key)
        {
            return resourceManager.GetString(key);

        }
    }
}
