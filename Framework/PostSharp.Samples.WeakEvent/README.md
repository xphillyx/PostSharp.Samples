This example demonstrates how to use the `EventInterceptionAspect` to implement a weak event pattern.

A weak event is an event that holds a weak reference to the delegate instead of a strong reference. 
Therefore, the weak event does not prevent the listener objects to be garbage collected.

Event handlers are often source of memory leaks in .NET. The reason is that the delegates registered as event handlers store a strong reference to the listener objects. 
The event source indirectly holds a strong reference to all listener objects.

In the weak event pattern, the event source does not hold a strong reference to the event listener. Instead, it stores a WeakReference
to the delegate. However, it is still necessary to ensure that there is at least one strong reference to the delegate, otherwise
it would get collected immediately upon the next GC cycle. In this example, we use a WeakConditionalTable to establish a strong reference
between the target of the delegate and the delegate itself. This makes sure that the delegate is alive until the delegate target instance
is alive.

Note that this approach does not work with some anonymous methods or lambda expressions because they are compiled as
closure classes. Nothing keeps alive the closure class instance itself. This case is not covered by this example.

The `[WeakEvent]` aspect is based on `EventInterceptionAspect` class. It implements the `IInstanceScopedAspect`
interface because the aspect needs to store the list of handlers for each instance of the event.


## Limitations

This example has not been sufficiently tested for production use.
This example assumes that the delegate target instance is kept alive, therefore it does not work with anonymous methods
and lambda expressions that use a closure class.