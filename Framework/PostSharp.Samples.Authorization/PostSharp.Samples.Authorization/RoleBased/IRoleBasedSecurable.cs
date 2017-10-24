using PostSharp.Patterns.Contracts;
using PostSharp.Samples.Authorization.Framework;

namespace PostSharp.Samples.Authorization.RoleBased
{
    public interface IRoleBasedSecurable
    {
        IRoleBasedSecurable SecurityParent { get; }

        bool HasRole([Required] ISubject subject, [Required] IRole role);
    }
}