using System;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Formatters;
using IFormattable = PostSharp.Patterns.Formatters.IFormattable;

namespace PostSharp.Samples.Logging
{
    class ExampleFormattable : IFormattable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        void IFormattable.Format(UnsafeStringBuilder stringBuilder, FormattingRole role)
        {
            stringBuilder.Append("{ExampleFormattable FirstName=\"");
            stringBuilder.Append(this.FirstName);
            stringBuilder.Append("\", LastName=\"");
            stringBuilder.Append(this.LastName);
            stringBuilder.Append("}");
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public static void Greet(ExampleFormattable formattable)
        {
            Console.WriteLine($"Hello, {formattable}.");
        }
    }
}
