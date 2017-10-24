using PostSharp.Samples.Authorization.RoleBased;

namespace PostSharp.Samples.Authorization.BusinessObjects
{
    public class BusinessUnit : Entity
    {
        public BusinessUnit ParentUnit { get; set; }
        public string Name { get; set; }
        public override IRoleBasedSecurable SecurityParent => this.ParentUnit;

        public override string ToString()
        {
            return this.Name;
        }
    }
}