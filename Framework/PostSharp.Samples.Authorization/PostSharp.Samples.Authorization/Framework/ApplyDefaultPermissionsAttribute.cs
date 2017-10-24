using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace PostSharp.Samples.Authorization.Framework
{
    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
    [AttributeUsage(AttributeTargets.Class)]
    public class ApplyDefaultPermissionsAttribute : MulticastAttribute, IAspectProvider
    {
        private static readonly RequiresPermissionAttribute permissionFactory = new RequiresPermissionAttribute(StandardPermission.Read, StandardPermission.Write);

        public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            var targetType = (Type) targetElement;

            foreach (var location in targetType.GetFields(BindingFlags.Instance | BindingFlags.Public).Cast<MemberInfo>().Union(
                targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Cast<MemberInfo>()))
            {
                
                if ( location.IsDefined(typeof(RequiresPermissionBaseAttribute), true) )
                    continue;

                var aspect = new LocationAuthorizationAspect();
                aspect.AddPermission(OperationSemantic.Read, 0, permissionFactory );
                aspect.AddPermission(OperationSemantic.Write, 0, permissionFactory);

                yield return new AspectInstance(location, aspect);
            }
        }

        
    }
}