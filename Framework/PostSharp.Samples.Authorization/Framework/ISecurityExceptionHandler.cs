namespace PostSharp.Samples.Authorization.Framework
{
    public interface ISecurityExceptionHandler
    {
        void OnSecurityException(ISubject subject, IPermission permission, object securable);
    }
}