#region Copyright (c) 2012 by SharpCrafters s.r.o.

// Copyright (c), SharpCrafters s.r.o. All rights reserved.

#endregion

using System;
using System.Collections.Immutable;
using System.Threading;
using PostSharp.Serialization;

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
                this.cleanUpCounter = 0;
                this.initialized = true;
                this.handlers = ImmutableArray<object>.Empty;
            }
        }

        public bool AddHandler(Delegate handler, bool weak)
        {
            bool lockTaken = false;
            
            try
            {
                this.spinLock.Enter(ref lockTaken);

                this.handlers = this.handlers.Add(weak ? (object) new WeakReference(handler) : handler);

                return this.handlers.Length == 1;

            }
            finally
            {
                if (lockTaken)
                {
                    this.spinLock.Exit();
                }

            }
          
        }

        public bool IsEmpty {  get { return this.handlers.IsEmpty; } }

        public bool RemoveHandler(Delegate handler)
        {
            bool lockTaken = false;
            try
            {
                this.spinLock.Enter(ref lockTaken);

                this.handlers = handlers.RemoveAll(o => o == handler || (o is WeakReference) && ((WeakReference)o).Target == handler);

                return this.handlers.IsEmpty;
            }
            finally
            {
                if (lockTaken)
                {
                    this.spinLock.Exit();
                }

            }
        }


        public void InvokeHandler(object[] args)
        {
            int lastCleanUpCounter = -1;


            // Take a snapshot of the handlers list.
            var invocationList = this.handlers;

            bool needCleanUp = false;

            foreach (object obj in invocationList)
            {
                Delegate handler = obj as Delegate;

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

            if (needCleanUp && lastCleanUpCounter == this.cleanUpCounter)
            {
                if (lastCleanUpCounter == this.cleanUpCounter)
                {
                    bool lockTaken = false;
                    try
                    {
                        this.spinLock.Enter(ref lockTaken);
                        this.handlers = this.handlers.RemoveAll(w => w is WeakReference && !((WeakReference) w).IsAlive);

#pragma warning disable 420
                        Interlocked.Increment(ref this.cleanUpCounter);
#pragma warning restore 420

                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            this.spinLock.Exit();
                        }
                    }
                }
            }
        }

     
    }
}