using System;
using System.Collections.Immutable;
using System.Threading;
using PostSharp.Serialization;

#pragma warning disable 420

namespace PostSharp.Samples.WeakEvent
{
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

        public bool AddHandler(Delegate handler)
        {
            var lockTaken = false;

            try
            {
                spinLock.Enter(ref lockTaken);

                handlers = handlers.Add(new WeakReference(handler));

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
                        o => ReferenceEquals(((WeakReference) o).Target, handler));

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
              
                var handler = (Delegate) ((WeakReference) obj).Target;

                if (handler == null)
                {
                    if (!needCleanUp)
                    {
                        needCleanUp = true;
                        lastCleanUpCounter = cleanUpCounter;
                    }

                    continue;
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
                        handlers = handlers.RemoveAll(w => !((WeakReference) w).IsAlive);

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