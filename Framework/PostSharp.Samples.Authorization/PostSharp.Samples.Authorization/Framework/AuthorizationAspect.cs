using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using PostSharp.Aspects;
using PostSharp.Patterns.Contracts;

namespace PostSharp.Samples.Authorization.Framework
{
    [Serializable]
    public abstract class AuthorizationAspect : IAspect
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<OperationPermission<IPermissionFactory>> _permissionFactories = new List<OperationPermission<IPermissionFactory>>();

        private OperationPermission<IPermission>[] _permissions;
        private BitArray _hasPermissionForParameter;

        [ThreadStatic]
        private static bool evaluatingPermissions;

        internal void AddPermission(OperationSemantic semantic, int parameterIndex, IPermissionFactory permissionFactory)
        {
            _permissionFactories.Add(new OperationPermission<IPermissionFactory>(semantic,  parameterIndex,  permissionFactory));
        }

        internal void InitializePermissions(int parameterCount, OperationSemantic[] semantics)
        {

            _hasPermissionForParameter = new BitArray(parameterCount);
            _permissions = new OperationPermission<IPermission>[_permissionFactories.Count * semantics.Length];

            var i = 0;
            foreach ( var permissionFactory in _permissionFactories )
            foreach ( var semantic in semantics )
            {
                _permissions[i] = new OperationPermission<IPermission>(semantic, permissionFactory.ParameterIndex,
                    permissionFactory.Permission.CreatePermission(semantic));

                _hasPermissionForParameter[permissionFactory.ParameterIndex] = true;
                 i++;
            }
            
        }

        internal bool HasPermissionForParameter(int parameterIndex)
        {
            return _hasPermissionForParameter[parameterIndex];
        }

        internal void RequirePermission(OperationSemantic semantic, int parameterIndex, object securable)
        {
            if (evaluatingPermissions)
                return;

            if (SecurityContext.Current == null)
                return;

            var subject = SecurityContext.Current.Subject;
            var policy = SecurityContext.Current.Policy;

            if (policy == null)
                return;


            try
            {
                evaluatingPermissions = true;

                foreach (var permission in _permissions)
                {
                    if ((permission.Semantic == OperationSemantic.Default || permission.Semantic == semantic) &&
                        permission.ParameterIndex == parameterIndex)
                    {
                        if (!policy.Evaluate(subject, permission.Permission, securable))
                        {
                            SecurityContext.Current.ExceptionHandler?.OnSecurityException(SecurityContext.Current.Subject, permission.Permission, securable);


                            throw new SecurityException($"The subject '{subject.Name}' does not have the {permission.Permission.Name} permission on the object '{securable}'.");
                        }
                    }
                }
            }
            finally
            {
                evaluatingPermissions = false;

            }
        }
        

        [Serializable]
        private struct OperationPermission<T>
        {
            private OperationSemantic _semantic;
            private int _parameterIndex;
            private T _permission;

            public OperationPermission(OperationSemantic semantic, int parameterIndex, [Required] T permission)
            {
                _semantic = semantic;
                _permission = permission;
                _parameterIndex = parameterIndex;
            }

            public OperationSemantic Semantic => _semantic;

            public int ParameterIndex => _parameterIndex;

            public T Permission => _permission;
        }

    }
}