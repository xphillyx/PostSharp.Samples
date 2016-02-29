using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharp.Samples.CustomLogging
{
    public static class Logger
    {
        private static int indentLevel;

        public static void Indent()
        {
            indentLevel++;
        }

        public static void Unindent()
        {
            indentLevel--;
        }

        public static void WriteLine(string message)
        {
            Console.Write(new string(' ', 3*indentLevel));
            Console.WriteLine(message);
        }
    }
}
