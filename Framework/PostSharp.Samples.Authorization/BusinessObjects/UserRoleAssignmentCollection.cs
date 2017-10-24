using PostSharp.Samples.Authorization.Framework;
using PostSharp.Samples.Authorization.RoleBased;

namespace PostSharp.Samples.Authorization.BusinessObjects
{
    public class UserRoleAssignmentCollection : System.Collections.ObjectModel.Collection<SubjectRoleAssignment>, IRoleBasedSecurable
    {
        public UserRoleAssignmentCollection(Entity parent)
        {
            Parent = parent;
        }

        [RequiresPermission(StandardPermission.ManageRoles)]
        protected override void ClearItems()
        {
            base.ClearItems();
        }

        [RequiresPermission(StandardPermission.ManageRoles)]
        protected override void InsertItem(int index, SubjectRoleAssignment item)
        {
            base.InsertItem(index, item);
        }

        [RequiresPermission(StandardPermission.ManageRoles)]
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        [RequiresPermission(StandardPermission.ManageRoles)]
        protected override void SetItem(int index, SubjectRoleAssignment item)
        {
            base.SetItem(index, item);
        }

        public void Add(User user, Role role)
        {
            this.Add(new SubjectRoleAssignment(user, role));
        }

        public Entity Parent { get; }

        IRoleBasedSecurable IRoleBasedSecurable.SecurityParent => this.Parent;

        public bool HasRole(ISubject subject, IRole role)
        {
            return this.Parent.HasRole(subject, role);
        }
    }
}