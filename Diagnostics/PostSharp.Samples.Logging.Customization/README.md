# PostSharp.Samples.Logging.Customization

This example demonstrates several ways to override the default behavior of PostSharp Logging:

* A value formatter named `FancyIntFormatter`, which demonstrates the implementation of the `Formatter` class.
* A formattable class named `ExampleFormattable`, which demonstrates the implementation of the `IFormattable` interface.
* A custom backend derived named `CustomLoggingBackend` which derives from `ConsoleLoggingBackend` but adds the thread id to the record line and a prefix to the parameter.



