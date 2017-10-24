using System;

namespace PostSharp.Samples.Authorization.Framework
{
    public interface IPermission : IEquatable<IPermission>
    {
        string Name { get; }
    }
}