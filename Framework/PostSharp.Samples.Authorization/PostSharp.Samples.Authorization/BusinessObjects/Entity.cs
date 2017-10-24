using System.Linq;
using PostSharp.Samples.Authorization.Framework;
using PostSharp.Samples.Authorization.RoleBased;

namespace PostSharp.Samples.Authorization.BusinessObjects
{

    [ApplyDefaultPermissions]
    public abstract class Entity : IRoleBasedSecurable
    {
        protected Entity()
        {
            this.UserRoleAssignments = new UserRoleAssignmentCollection(this);
        }

        [RequiresPermission(StandardPermission.Read, StandardPermission.Assign)]
        public User Owner { get; set; }

        public UserRoleAssignmentCollection UserRoleAssignments { get;  }


        public abstract IRoleBasedSecurable SecurityParent { get; }

        public virtual bool HasRole(ISubject subject, IRole role)
        {
            if (role.Equals(Role.Owner))
            {
                return subject.Equals(this.Owner);
            }
            else if (this.UserRoleAssignments.Any(a => a.Role.Equals(role) && a.Subject.Equals(subject)) )
            {
                return true;
            }
            else if (this.SecurityParent != null)
            {
                return this.SecurityParent.HasRole(subject, role);
            }
            else
            {
                return false;
            }
        }

    }
}
