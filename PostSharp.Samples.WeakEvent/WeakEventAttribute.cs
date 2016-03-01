using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Samples.WeakEvent
{
    [PSerializable]
    public sealed class WeakEventAttribute : EventInterceptionAspect, IInstanceScopedAspect
    {
        [PNonSerialized]
        private WeakEventHandler weakEventHandler;

        public bool AllowStrongReferences { get; set; }

        public WeakEventAttribute(bool allowStrongReferences = false)
        {
            this.AllowStrongReferences = allowStrongReferences;
        }


        public object CreateInstance(AdviceArgs adviceArgs)
        {
            return this.MemberwiseClone();
        }

        public void RuntimeInitializeInstance()
        {
            this.weakEventHandler.Initialize();    
        }

        public override void RuntimeInitialize(EventInfo eventInfo)
        {
            if (eventInfo.AddMethod.IsStatic)
            {
                this.weakEventHandler.Initialize();
            }
            else
            {
                // The method RuntimeInitializeInstance will be called.
            }
        }

        public override void OnAddHandler(EventInterceptionArgs args)
        {
            IWeakEventClient weakEventClient = args.Handler.Target as IWeakEventClient;

            bool supportsWeakReference = weakEventClient != null;

            if (!supportsWeakReference && args.Handler.Target != null && !this.AllowStrongReferences )
            {
                throw new InvalidOperationException(string.Format("Attempt to add a reference to the weak event {0} from type {1}, which does not implement the IWeakEventClient interface.", args.Event, args.Handler.Target.GetType()));
            }

            if (this.weakEventHandler.AddHandler(args.Handler, supportsWeakReference))
            {
                args.AddHandler(null);
            }

            if (supportsWeakReference)
            {
                weakEventClient.RegisterEventHandler(args.Handler);
            }
        }

        public override void OnRemoveHandler(EventInterceptionArgs args)
        {
            IWeakEventClient weakEventClient = args.Handler.Target as IWeakEventClient;

            bool isWeak = weakEventClient != null;

            if (this.weakEventHandler.RemoveHandler(args.Handler))
            {
                args.RemoveHandler(null);
            }

            if (isWeak)
            {
                weakEventClient.UnregisterEventHandler(args.Handler);
            }
        }

        public override void OnInvokeHandler(EventInterceptionArgs args)
        {
            this.weakEventHandler.InvokeHandler(args.Arguments.ToArray());
        }
    }
}
