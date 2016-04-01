# PostSharp.Samples.WeakEvent

This example demonstrates how to use the `EventInterceptionAspect` to implement a weak event pattern.

Event handlers are often source of memory leaks in .NET. The reason is that the delegates registered as event handlers store a strong reference to the object consuming the event, and therefore the object exposing the event indirectly
holds a strong reference to all objects consuming the event.

A weak event is an event that holds a weak reference to the delegate instead of a strong reference. Therefore, the weak event does not prevent the event consumers to be garbage collected.

Implementing the weak event pattern requires cooperation of the exposing and the consuming objects. If the exposing object only stores weak references to the delegates, the delegates will be immediately collected even if the consumer object
is still alive. To prevent immediate garbage collection of delegates, the consumer should hold strong references to all delegates passed to the weak events.

The pattern is implemented by the following artefacts:

* `[WeakEvent]` is an aspect that can be applied to any event. This aspect is based on `EventInterceptionAspect` and implements the `IInstanceScopedAspect` interface to store instance-scoped data.
* `[WeakEventClient]` is an aspect that should be applied to any consumer of a weak event. It automatically implements the `IWeakEventClient` interface. This aspect demonstrates the following features: `InstanceLevelAspect`, `IntroduceInterface`.

By default, the `WeakEvent` aspect will throw an exception if the consumer class does not have the `WeakEventClient` aspect or does not manually implement the `IWeakEventClient` interface. 


## Limitations

This example has not been sufficiently tested for production use.
