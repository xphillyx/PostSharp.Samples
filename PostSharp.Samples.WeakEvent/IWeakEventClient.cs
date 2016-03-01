using System;

namespace PostSharp.Samples.WeakEvent
{
    public interface IWeakEventClient
    {
        void RegisterEventHandler(Delegate handler);
        void UnregisterEventHandler(Delegate handler);
    }
}
