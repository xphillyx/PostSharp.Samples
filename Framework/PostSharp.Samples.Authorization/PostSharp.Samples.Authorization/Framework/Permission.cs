using System;

namespace PostSharp.Samples.Authorization.Framework
{
    public class Permission : IPermission
    {
        public static readonly Permission Read = new Permission(StandardPermission.Read.ToString());
        public static readonly Permission Write = new Permission(StandardPermission.Write.ToString());
        public static readonly Permission Assign = new Permission(StandardPermission.Assign.ToString());
        public static readonly Permission ManageRoles = new Permission(StandardPermission.ManageRoles.ToString());

        public Permission(string name)
        {
            Name = name;
        }

        public string Name { get; }

        bool IEquatable<IPermission>.Equals(IPermission other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Permission) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}