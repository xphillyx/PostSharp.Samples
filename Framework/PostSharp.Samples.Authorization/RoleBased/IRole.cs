using System;

namespace PostSharp.Samples.Authorization.RoleBased
{
    public interface IRole  : IEquatable<IRole>
    {
        Guid Id { get; }

        string Name { get; }
    }
}