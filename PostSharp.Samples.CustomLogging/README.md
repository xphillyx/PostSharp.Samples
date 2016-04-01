# PostSharp.Samples.CustomLogging

Have you ever had the problem where your program misbehaves and you just do not have a debugger attached
at the moment? Or that some bug is appearing occasionally and you just don't know how to reproduce
such case manually? Usual helper here is logging. But introducing logging into your code comes with
a lot of manual work and adds a lot of boilerplate. And there is also the dilemma of deciding which logic
should be traced and at which level of detail.

All this pain is taken away with PostSharp. This example demonstrates how to use
the `OnMethodBoundaryAspect` and `LocationInterceptionAspect` classes to build aspects that log
the operations of invoking a method and setting the value of a field or property. It shows how to get
information about the calling context thanks to `args.Arguments`, `args.Method` or `args.Location`.

## Limitations

This example is purely didactic. It has the following limitations:

* Not optimized for run-time performance.
* Not thread-safe.
* Subject to infinite loops if you add logging to the `ToString` method.

If you need production-grade logging, you should consider using the
`PostSharp.Patterns.Diagnostics.LogAspect` class. See http://doc.postsharp.net/logging for details.
Our ready-made aspects work out of the box. Just follow our wizard, tell it your expectations
and the whole logging gets set up and implemented for you in a few minutes without modifying your
existing source code.


