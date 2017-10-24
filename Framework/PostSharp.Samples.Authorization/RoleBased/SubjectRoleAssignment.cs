using PostSharp.Patterns.Contracts;
using PostSharp.Samples.Authorization.Framework;

namespace PostSharp.Samples.Authorization.RoleBased
{
    public class SubjectRoleAssignment
    {
        public SubjectRoleAssignment([Required] ISubject subject, [Required] IRole role)
        {
            Role = role;
            Subject = subject;
        }

        public IRole Role { get; }
        public ISubject Subject { get; }
    }
}