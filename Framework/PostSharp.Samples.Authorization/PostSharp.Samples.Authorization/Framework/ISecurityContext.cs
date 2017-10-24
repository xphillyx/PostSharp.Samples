namespace PostSharp.Samples.Authorization.Framework
{
    public interface ISecurityContext
    {
        ISubject Subject { get; }

        ISecurityPolicy Policy { get; }

        ISecurityExceptionHandler ExceptionHandler { get; }
    }
}