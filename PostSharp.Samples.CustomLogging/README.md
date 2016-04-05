# PostSharp.Samples.CustomLogging

Have you ever had the problem where your program misbehaves and you just do not have a debugger attached
at the moment? Or that some bug is appearing occasionally and you just don't know how to reproduce
such a case manually? The usual solution here is logging. But introducing logging into your code comes with
a lot of manual work and adds a lot of boilerplate. There is also the dilemma of deciding which logic
should be traced and at which level of detail.

All this pain is taken away with PostSharp.

When you apply the `[LogMethod]` aspect to a method, the method creates a log record when:
* the method is invoked,
* the method returns succesfully,
* the method fails with an exception.

TODO: LogSetValueAttribute

## What is being demonstrated?

The example uses the following PostSharp features:

* The `OnMethodBoundaryAspect` class ...
* The `LocationInterceptionAspect` class ...
* The `args.Arguments`, `args.Method` or `args.Location` ...

(and  classes are used to build aspects that log
the operations of invoking a method and setting the value of a field or property. The example shows how to get
information about the calling context thanks to .)

## Limitations

This example is purely educational. It has the following limitations:

* Not optimized for run-time performance.
* Not thread-safe.
* Subject to infinite loops if you add logging to the `ToString` method.

If you need production-grade logging, you should consider using the
`PostSharp.Patterns.Diagnostics.LogAttribute` aspect.

The `LogAttribute` aspect works out of the box. The `LogAttribute` aspect is highly optimized for run-time performance, multithreading and reentrance. You can set up logging in a few minutes without modifying your existing source code. TODO: backends (shortly)

See http://doc.postsharp.net/logging.


