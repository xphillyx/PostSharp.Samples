using System;
using System.Collections.Generic;
using PostSharp.Samples.Authorization.Framework;

namespace PostSharp.Samples.Authorization.RoleBased
{


    public class RoleBasedSecurityPolicy : ISecurityPolicy
    {
        readonly Dictionary<Type, List<RolePermissionAssignment>> rolePermissionAssignments = new Dictionary<Type, List<RolePermissionAssignment>>();

        public void AddRolePermissionAssignment(RolePermissionAssignment assignment)
        {
            List<RolePermissionAssignment> list;
            if (!rolePermissionAssignments.TryGetValue(assignment.Entitytype, out list))
            {
                list = new List<RolePermissionAssignment>();
                rolePermissionAssignments.Add(assignment.Entitytype, list);
            }

            list.Add(assignment);
        }

        public bool Evaluate(ISubject subject, IPermission permission, object securable)
        {
            var securableAncestor = (IRoleBasedSecurable) securable;

            for (var type = securableAncestor.GetType(); type != null; type = type.BaseType)
            {
                List<RolePermissionAssignment> assignments;
                if (rolePermissionAssignments.TryGetValue(type, out assignments))
                {
                    foreach (var assignment in assignments)
                    {

                        if (assignment.Permission.Equals(permission) && securableAncestor.HasRole(subject, assignment.Role))
                        {
                            switch (assignment.Action)
                            {
                                case PermissionAction.Grant:
                                    return true;

                                case PermissionAction.Revoke:
                                    return false;
                            }
                        }

                    }
                }

            }

            return false;
        }
    }
}