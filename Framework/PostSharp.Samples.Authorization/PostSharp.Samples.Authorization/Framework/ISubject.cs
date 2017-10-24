using System;

namespace PostSharp.Samples.Authorization.Framework
{
    public interface ISubject : IEquatable<ISubject>
    {

        string Name { get; }

    }
}