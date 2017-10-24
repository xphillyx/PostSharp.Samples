using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Reflection;

namespace PostSharp.Samples.Authorization.Framework
{
    [Serializable]
    [AspectTypeDependency(AspectDependencyAction.Commute, typeof(LocationAuthorizationAspect))]
    public class LocationAuthorizationAspect : AuthorizationAspect, ILocationInterceptionAspect
    {
        
        internal LocationAuthorizationAspect()
        {
            
        }

        public void RuntimeInitialize(LocationInfo locationInfo)
        {
            InitializePermissions(1, new [] {OperationSemantic.Read, OperationSemantic.Write});
        }

        public void OnGetValue(LocationInterceptionArgs args)
        {
            RequirePermission(OperationSemantic.Read, 0, args.Instance);

            args.ProceedGetValue();
        }

        public void OnSetValue(LocationInterceptionArgs args)
        {
            RequirePermission(OperationSemantic.Write, 0, args.Instance);

            args.ProceedSetValue();
        }
        

       
    }
}