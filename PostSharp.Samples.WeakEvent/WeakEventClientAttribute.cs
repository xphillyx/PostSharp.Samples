using System;
using System.Collections.Immutable;
using System.Threading;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Serialization;

namespace PostSharp.Samples.WeakEvent
{
    /// <summary>
    ///     Aspect that, when added to a class, enables the class to subscribe to weak events built with the
    ///     <see cref="WeakEventAttribute" /> aspect.
    /// </summary>
    [PSerializable]
    [IntroduceInterface(typeof (IWeakEventClient))]
    public sealed class WeakEventClientAttribute : InstanceLevelAspect, IWeakEventClient
    {
        [PNonSerialized] private ImmutableArray<Delegate> handlers;

        [PNonSerialized] private SpinLock spinLock;

        /// <summary>
        ///     Initializes the aspect at run-time.
        /// </summary>
        /// <param name="type"></param>
        public override void RuntimeInitialize(Type type)
        {
            handlers = ImmutableArray<Delegate>.Empty;

            // We don't need to implement RuntimeInitializeInstance because this.handlers is an immutable collection,
            // so MemberwiseClone is safe here. If we had a mutable collection, we would have to initialize the 
            // collection in RuntimeInitializeInstance instance too.
        }

        #region Implementation of IWeakEventClient

        void IWeakEventClient.RegisterEventHandler(Delegate handler)
        {
            var lockTaken = false;

            try
            {
                spinLock.Enter(ref lockTaken);

                handlers = handlers.Add(handler);
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }
        }

        void IWeakEventClient.UnregisterEventHandler(Delegate handler)
        {
            var lockTaken = false;

            try
            {
                spinLock.Enter(ref lockTaken);

                handlers = handlers.Remove(handler);
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }
        }

        #endregion
    }
}