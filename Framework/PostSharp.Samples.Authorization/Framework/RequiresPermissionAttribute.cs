using System;

namespace PostSharp.Samples.Authorization.Framework
{
    [Serializable]
    public class RequiresPermissionAttribute : RequiresPermissionBaseAttribute
    {
        string name, writeName;

        public RequiresPermissionAttribute( StandardPermission permission )
        {
            this.name = permission.ToString();
        }

        public RequiresPermissionAttribute( string name )
        {
            this.name = name;
        }

        public RequiresPermissionAttribute(StandardPermission readPermission, StandardPermission writePermission)
        {
            this.name = readPermission.ToString();
            this.writeName = writePermission.ToString();
        }

        public RequiresPermissionAttribute(string readPermission, string writePermission)
        {
            this.name = readPermission;
            this.writeName = writePermission;
        }


        public override IPermission CreatePermission(OperationSemantic semantic)
        {
            switch (semantic)
            {
                case OperationSemantic.Default:
                case OperationSemantic.Read:
                    return new Permission(this.name);

                case OperationSemantic.Write:
                    return new Permission(this.writeName);

                default:
                    throw new ArgumentOutOfRangeException(nameof(semantic), semantic, null);
            }
            
        }
    }
}