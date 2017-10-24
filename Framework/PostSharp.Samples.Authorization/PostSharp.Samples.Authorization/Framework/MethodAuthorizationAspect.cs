using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace PostSharp.Samples.Authorization.Framework
{
    [Serializable]
    [AspectTypeDependency(AspectDependencyAction.Commute, typeof(MethodAuthorizationAspect))]
    public class MethodAuthorizationAspect : AuthorizationAspect, IOnMethodBoundaryAspect
    {
        public void RuntimeInitialize(MethodBase method)
        {
            InitializePermissions(method.GetParameters().Length+1, new []{ OperationSemantic.Default});
        }

        public void OnEntry(MethodExecutionArgs args)
        {
            if (this.HasPermissionForParameter(0))
            {
                RequirePermission(OperationSemantic.Default, 0, args.Instance);
            }

            for (var i = 0; i < args.Arguments.Count; i++)
            {
                RequirePermission(OperationSemantic.Default, i+1, args.Arguments[i]);
            }
        }

        public void OnExit(MethodExecutionArgs args)
        {
            // Nothing to do.
        }

        public void OnSuccess(MethodExecutionArgs args)
        {
            // Nothing to do.
        }


        public void OnException(MethodExecutionArgs args)
        {
            // Nothing to do.
        }
    }
}