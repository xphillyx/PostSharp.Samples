using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharp.Samples.NormalizeString
{
    class Program
    {
        [NormalizeString]
        static string myField;

        static void Main( string[] args )
        {

            myField = "   Hello, world.    ";

            Console.WriteLine( "\"" + myField + "\"" );
            
        }
    }
}
