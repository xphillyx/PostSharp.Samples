using System;
using System.Collections.Immutable;
using System.Threading;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Serialization;

namespace PostSharp.Samples.WeakEvent
{
    [PSerializable]
    [IntroduceInterface(typeof(IWeakEventClient))]
    public sealed class WeakEventClientAttribute : InstanceLevelAspect, IWeakEventClient
    {
        [PNonSerialized]
        private SpinLock spinLock;

        [PNonSerialized]
        private ImmutableArray<Delegate> handlers;

        public void RegisterEventHandler(Delegate handler)
        {
            bool lockTaken = false;

            try
            {
                this.spinLock.Enter(ref lockTaken);

                this.handlers = this.handlers.Add(handler);
            }
            finally
            {
                if (lockTaken)
                {
                    this.spinLock.Exit();
                }
            }    
        }

        public void UnregisterEventHandler(Delegate handler)
        {
            bool lockTaken = false;

            try
            {
                this.spinLock.Enter(ref lockTaken);

                this.handlers = this.handlers.Remove(handler);
            }
            finally
            {
                if (lockTaken)
                {
                    this.spinLock.Exit();
                }
            }
        }

        public override void RuntimeInitializeInstance()
        {
            this.handlers = ImmutableArray<Delegate>.Empty;
        }
    }
}
