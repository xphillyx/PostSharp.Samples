using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Formatters;

namespace MicroserviceExample
{
    public class ActionResultFormatter<T> : Formatter<ActionResult<T>>
    {
        public override void Write( UnsafeStringBuilder stringBuilder, ActionResult<T> value )
        {
            LoggingServices.Formatters.Get<T>().Write( stringBuilder, value.Value );
        }
    }

    public class ObjectResultFormatter : Formatter<ObjectResult>
    {
        public override void Write(UnsafeStringBuilder stringBuilder, ObjectResult value )
        {
            if ( value.Value != null )
            {
                LoggingServices.Formatters.Get(value.Value.GetType() ).Write(stringBuilder, value.Value );
            }
            else
            {
                stringBuilder.Append("null" );
            }
        }
    }

    public class ActionResultFormatter : Formatter<IActionResult>
    {
        public override void Write( UnsafeStringBuilder stringBuilder, IActionResult value )
        {
            IFormatter specificFormatter = LoggingServices.Formatters.Get(value.GetType() );
            if ( specificFormatter != this )
            {
                specificFormatter.Write(stringBuilder, value );
            }
            else
            {
                const string suffix = "Result";
                string name = value.GetType().Name;

                if ( name.EndsWith(suffix ) )
                {
                    name = name.Substring(0, name.Length - suffix.Length );
                }

                stringBuilder.Append('{' );
                stringBuilder.Append(name );
                stringBuilder.Append('}' );
            }
        }
    }
}
