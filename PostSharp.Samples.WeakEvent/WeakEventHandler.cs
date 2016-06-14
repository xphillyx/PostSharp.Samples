using System;
using System.Collections.Immutable;
using System.Threading;
using PostSharp.Serialization;

#pragma warning disable 420

namespace PostSharp.Samples.WeakEvent
{
    [PSerializable]
    internal struct WeakEventHandler
    {
        private bool initialized;
        private ImmutableArray<object> handlers;
        [PNonSerialized] private volatile int cleanUpCounter;
        private SpinLock spinLock;

        public void Initialize()
        {
            if (!initialized)
            {
                cleanUpCounter = 0;
                initialized = true;
                handlers = ImmutableArray<object>.Empty;
            }
        }

        public bool AddHandler(Delegate handler, bool weak)
        {
            var lockTaken = false;

            try
            {
                spinLock.Enter(ref lockTaken);

                handlers = handlers.Add(weak ? (object) new WeakReference(handler) : handler);

                return handlers.Length == 1;
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }
        }

        public bool RemoveHandler(Delegate handler)
        {
            var lockTaken = false;
            try
            {
                spinLock.Enter(ref lockTaken);

                handlers =
                    handlers.RemoveAll(
                        o =>
                            ReferenceEquals(o, handler) ||
                            o is WeakReference && ReferenceEquals(((WeakReference) o).Target, handler));

                return handlers.IsEmpty;
            }
            finally
            {
                if (lockTaken)
                {
                    spinLock.Exit();
                }
            }
        }


        public void InvokeHandler(object[] args)
        {
            var lastCleanUpCounter = -1;


            // Take a snapshot of the handlers list.
            var invocationList = handlers;

            var needCleanUp = false;

            foreach (var obj in invocationList)
            {
                var handler = obj as Delegate;

                if (handler == null)
                {
                    handler = (Delegate) ((WeakReference) obj).Target;

                    if (handler == null)
                    {
                        if (!needCleanUp)
                        {
                            needCleanUp = true;
                            lastCleanUpCounter = cleanUpCounter;
                        }

                        continue;
                    }
                }


                handler.DynamicInvoke(args);
            }

            if (needCleanUp && lastCleanUpCounter == cleanUpCounter)
            {
                if (lastCleanUpCounter == cleanUpCounter)
                {
                    var lockTaken = false;
                    try
                    {
                        spinLock.Enter(ref lockTaken);
                        handlers = handlers.RemoveAll(w => w is WeakReference && !((WeakReference) w).IsAlive);

                        Interlocked.Increment(ref cleanUpCounter);
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            spinLock.Exit();
                        }
                    }
                }
            }
        }
    }
}