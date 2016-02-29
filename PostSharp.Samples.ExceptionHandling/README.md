# PostSharp.Samples.ExceptionHandling

This example demonstrates how to use the `OnExceptionAspect` class to handle exceptions. It contains two aspects:

* `AddContextOnExceptionAttribute` is meant to be added to all methods in the assembly using the one-liner `[assembly:AddContextOnException]`. Its role is to add the parameter values of the current value to the `Exception.Data["Context"]` object.
   Since the aspect is supposed to be applied to all user-writen methods, the aspect will gather the parameter values of all user-writen methods in the call stack, and it will be a great help for debugging. This aspect
   does not suppress the exception.
   
* `ReportAndSwallowExceptionAttribute` writes the exception to the console (including the data collected by  `AddContextOnExceptionAttribute`) and suppresses the exception. Suppressing exceptions is dangerous and you cannot do that on each
   method. You would typically use this kind of aspects on an event handler (such as WPF and WinForms ones) or a thread entrypoint. 

