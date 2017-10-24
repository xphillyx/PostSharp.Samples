using System;
using PostSharp.Samples.Authorization.Framework;

namespace PostSharp.Samples.Authorization.RoleBased
{
    public class RolePermissionAssignment
    {
        public RolePermissionAssignment(Type entitytype, IPermission permission, IRole role, PermissionAction action)
        {
            Action = action;
            Permission = permission;
            Role = role;
            Entitytype = entitytype;
        }

        public Type Entitytype { get; }

        public PermissionAction Action { get; }

        public IPermission Permission { get; }

        public IRole Role { get; }

        public override string ToString()
        {
            return $"{this.Action} {this.Permission} to {this.Role}";
        }
    }
}