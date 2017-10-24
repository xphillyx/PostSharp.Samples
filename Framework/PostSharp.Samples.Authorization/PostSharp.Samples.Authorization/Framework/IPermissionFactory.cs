namespace PostSharp.Samples.Authorization.Framework
{
    public interface IPermissionFactory
    {
        IPermission CreatePermission(OperationSemantic semantic);
    }
}