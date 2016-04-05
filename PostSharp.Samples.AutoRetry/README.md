# PostSharp.Samples.AutoRetry

This examples demonstrates an implementation and usage of `AutoRetry` aspect.

You can use this aspect to improve the stability of your application by making it more resilient to anticipated, temporary failures. `AutoRetry` aspect is most often applied to code that attempts to connect to a remote service or a network resource. The aspect then transparently retries each failed operation in the expectation that the cause of the failure is temporary. The maximum number of retries can be configured.

The `AutoRetry` aspect implementation demonstrates how to use the `MethodInterceptionAspect` and also shows how to use custom attribute properties to customize the aspect.

