namespace PostSharp.Samples.Authorization.Framework
{
    public interface ISecurityPolicy
    {
        bool Evaluate(ISubject subject, IPermission permission, object securable);
    }
}