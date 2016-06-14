using System;

namespace PostSharp.Samples.WeakEvent
{
    /// <summary>
    ///     Interface that allows clients of weak events (i.e. events that have a <see cref="WeakEventAttribute" /> aspect) to
    ///     hold references
    ///     to their own event handlers, preventing gargage collection of these handlers.
    /// </summary>
    public interface IWeakEventClient
    {
        void RegisterEventHandler(Delegate handler);
        void UnregisterEventHandler(Delegate handler);
    }
}