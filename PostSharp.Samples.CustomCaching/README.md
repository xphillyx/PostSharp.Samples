# PostSharp.Samples.CustomCaching

This example demonstrates how to use the `FlowBehavior` and `MethodExecutionTag` properties of the `MethodExecutionArgs` class to
conditionally skip the execution of a method and store a value (here the cache key) between the `OnEntry` and `OnSuccess` advices.


## Limitations

This example is purely didactic. It has the following limitations:

* Not optimized for run-time performance.
* Simplistic algorithm to compute the cache key.
* No dependency management.
* No cache eviction.
