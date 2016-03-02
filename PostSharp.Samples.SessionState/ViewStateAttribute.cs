using System;
using System.Web;
using System.Web.UI;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using PostSharp.Serialization;

namespace PostSharp.Samples.SessionState
{
    [PSerializable]
    [LinesOfCodeAvoided(3)]
    [MulticastAttributeUsage(TargetTypeAttributes = MulticastAttributes.Instance)]
    public sealed class ViewStateAttribute : LocationInterceptionAspect, IInstanceScopedAspect
    {
        private string name;

        public override bool CompileTimeValidate(LocationInfo locationInfo)
        {
            if (!typeof (Control).IsAssignableFrom(locationInfo.DeclaringType))
            {
                Message.Write(locationInfo, SeverityType.Error, "VS01", "Cannot apply [ViewState] to {0} because the declaring type is not a Control or Page.", locationInfo);
                return false;
            }

            return true;
        }

        public override void CompileTimeInitialize(LocationInfo targetLocation, AspectInfo aspectInfo)
        {
            name = targetLocation.DeclaringType.FullName + "." + targetLocation.Name;
        }

        [ImportMember("ViewState")]
        public Property<StateBag> ViewState;

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            var value = this.ViewState.Get()[this.name];

            if (value != null)
            {
                args.Value = value;
            }
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            this.ViewState.Get()[this.name] = args.Value;
        }

        object IInstanceScopedAspect.CreateInstance(AdviceArgs adviceArgs)
        {
            return this.MemberwiseClone();
        }

        void IInstanceScopedAspect.RuntimeInitializeInstance()
        {
            
        }
    }
}