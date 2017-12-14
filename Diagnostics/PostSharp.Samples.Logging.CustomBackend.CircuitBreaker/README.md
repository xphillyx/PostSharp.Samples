This sample shows prevents defects in the logging framework or in PostSharp Logging to affect your application. It adds a circuit breaker between your application and PostSharp Logging.

We at PostSharp consider this sample as imperfect and will work to make this scenario work better
in a next release. The benefit of this approach is that it works with the current stable release of PostSharp 5.0. 

## How it works

The sample implements a custom logging back-end (i.e. an adapter to a logging framework) by deriving all classes
of a randomly chosen back-end, here NLog. The custom back-end overrides all top-level methods. 

The `[LoggingCircuitBreaker]` aspect is applied to all methods of the custom back-end. This aspect does two things:

  1. Upon exception, it breaks the circuit breaker (implemented in the `LoggingCircuitBreaker` class) and
     'swallows' the exception.

  2. On entry, it skips the method execution if the circuit breaker is open.

## Changing for a different logging back-end

If you want to use the circuit breaking adapter with a different back-end than NLog, you have to
modify the base classes of the following classes:

* `CircuitBreakingLoggingBackend`
* `CircuitBreakingLogRecordBuilder`
* `CircuitBreakingLoggingTypeSource`

This would be sufficient for most logging back-ends. With Serilog, you additionally need to change
the base class of all context classes.

## Limitations

* This sample is almost untested.

* Due to design limitations of PostSharp 5.0, the circuit breaking logging adapter needs to be implemented by
inheritance, while a design based on a chain of responsibility would be better. 

