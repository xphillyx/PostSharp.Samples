using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Serialization;

namespace PostSharp.Samples.SessionState
{
    [PSerializable]
    [LinesOfCodeAvoided(3)]
    public sealed class SessionStateAttribute : LocationInterceptionAspect
    {
        private string name;

        public override void CompileTimeInitialize(LocationInfo targetLocation, AspectInfo aspectInfo)
        {
            name = targetLocation.DeclaringType.FullName + "." + targetLocation.Name;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            var value = HttpContext.Current.Session[this.name];
            if (value != null)
            {
                args.Value = value;
            }
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            HttpContext.Current.Session[this.name] = args.Value;
        }
    }
}