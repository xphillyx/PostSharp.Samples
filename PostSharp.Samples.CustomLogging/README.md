# PostSharp.Samples.CustomLogging

This example demonstrates how to use the `OnMethodBoundaryAspect` and `LocationInterceptionAspect` classes
to build aspects that log the operations of invoking a method and setting the value of a field or property.
It shows how to get information about the calling context thanks to `args.Arguments`,  `args.Method` or
`args.Location`.

## Limitations

This example is purely didactic. It has the following limitations:

* Not optimized for run-time performance.
* Not thread-safe.
* Subject to infinite loops if you add logging to the `ToString` method.

If you need production-grade logging, you should consider using the
`PostSharp.Patterns.Diagnostics.LogAspect` class. See http://doc.postsharp.net/logging for details.


