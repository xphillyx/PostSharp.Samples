using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PostSharp.Samples.WeakEvent
{
    /// <summary>
    ///     Aspect that, when applied to an event, prevents the target event from holding a strong reference to event handlers.
    ///     Therefore, the aspect prevents the event to prevent clients to be garbage collected.
    ///     Client classes must have the <see cref="WeakEventClientAttribute" /> aspect, otherwise an exception will be thrown
    ///     at runtime (if <see cref="AllowStrongReferences" /> is set to <c>true</c>), or a
    ///     strong reference will be created.
    /// </summary>
    [PSerializable]
    [LinesOfCodeAvoided(6)]
    public sealed class WeakEventAttribute : EventInterceptionAspect, IInstanceScopedAspect
    {
        [PNonSerialized] private WeakEventHandler weakEventHandler;


        /// <summary>
        ///     Creates an instance of the aspect for a specific instance of the target class (when the target event is not
        ///     static). A call of this method is followed by a call of RuntimeInitializeInstance.
        /// </summary>
        object IInstanceScopedAspect.CreateInstance(AdviceArgs adviceArgs)
        {
            return MemberwiseClone();
        }

        /// <summary>
        ///     Initializes the current instance of the aspect (when the target event is not static).
        /// </summary>
        void IInstanceScopedAspect.RuntimeInitializeInstance()
        {
            weakEventHandler.Initialize();
        }

        /// <summary>
        ///     Initializes the current instance of the aspect (whether target event is static or not).
        /// </summary>
        public override void RuntimeInitialize(EventInfo eventInfo)
        {
            if (eventInfo.AddMethod.IsStatic)
            {
                weakEventHandler.Initialize();
            }
        }

        /// <summary>
        ///     Method invoked when (instead of) a new handler is added to the target event.
        /// </summary>
        /// <param name="args">Context information.</param>
        public override void OnAddHandler(EventInterceptionArgs args)
        {
           

            // Add the handler to our own list.
            if (weakEventHandler.AddHandler(args.Handler))
            {
                // If it is the first handler we are adding, add a fake handler to ourselves to the target event.
                // Whichever handler will add here will be passed to OnInvokeHandler, so it is safe and convenient to pass null.
                args.AddHandler(null);
            }


            // Register the handler to the client to prevent garbage collection of the handler.
            DelegateReferenceKeeper.AddReference(args.Handler);
        }

        /// <summary>
        ///     Method invoked when (instead of) a new handler is removed from the target event.
        /// </summary>
        /// <param name="args">Context information.</param>
        public override void OnRemoveHandler(EventInterceptionArgs args)
        {
             // Remove the handler from our own list.
            if (weakEventHandler.RemoveHandler(args.Handler))
            {
                // If this is the last handler, remove the fake handler to ourselves from the target event.
                args.RemoveHandler(null);
            }

            // Remove the handler from the client.
            DelegateReferenceKeeper.RemoveReference(args.Handler);
        }

        /// <summary>
        ///     Method invoked when (instead of) a the target event is raised.
        /// </summary>
        /// <param name="args">Context information.</param>
        public override void OnInvokeHandler(EventInterceptionArgs args)
        {
            // Note that args.Handler == null because it's the value we added in OnAddHandler, but it really does not matter.

            weakEventHandler.InvokeHandler(args.Arguments.ToArray());
        }

    }
}