using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace PostSharp.Samples.Authorization.Framework
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public abstract class RequiresPermissionBaseAttribute : Attribute, IPermissionFactory, IAspectProvider
    {
        public abstract IPermission CreatePermission(OperationSemantic semantic);

        public OperationSemantic Semantic { get; set; }

        public virtual IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            var aspectRepositoryService =
                PostSharpEnvironment.CurrentProject.GetService<IAspectRepositoryService>();

            var aspects = (aspectRepositoryService.GetAspectInstances(targetElement) ?? new IAspectInstance[0]);
                

            var aspect =  (AuthorizationAspect) aspects.Where(i => i != null) // TODO: Fix bug in PostSharp and remove this.
                .Select(i => i.Aspect).SingleOrDefault(i => i is AuthorizationAspect);

            if ( aspect == null )
            {

                if (targetElement is MethodBase)
                {
                    aspect = new MethodAuthorizationAspect();
                    
                }
                else if (targetElement is PropertyInfo || targetElement is FieldInfo)
                {
                    aspect = new LocationAuthorizationAspect();
                }
                else if (targetElement is ParameterInfo)
                {
                    aspect = new MethodAuthorizationAspect();
                }
                else
                {
                    throw new Exception($"Unexpected aspect target: {targetElement.GetType().Name}");
                }

                yield return new AspectInstance(targetElement, aspect);
            }

            var parameterIndex = 0;

            if (targetElement is ParameterInfo parameterInfo)
            {
                parameterIndex = parameterInfo.Position + 1;
            }

            aspect.AddPermission(this.Semantic, parameterIndex, this );
        }
    }
}