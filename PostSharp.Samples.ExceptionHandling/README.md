# PostSharp.Samples.ExceptionHandling

Handling exceptions is the bread and butter of every programmer, especially those using the code throwing exceptions. Surely it has happened to you. 
You had no idea when and what might the called method throw on you. Or worse, what something buried deep in the call tree may throw that the author of the method did not anticipate. 

Frankly, this happens in every application and gets worse as its lifecycle progresses. As the time flows, you are pushed to patch the code here 
and there, for this kind of exception and that. Mostly, you handle exceptions that have nothing to do with the code where you need to handle them. You log 
them, ignore them, add at least some information that may help track their cause in production. You can see that it is plain wrong to do it everywhere
by changing the actual code, but you had no other options.

Now PostSharp changes your situation completely - you get aspects.

In this example you will first see how to use the `OnExceptionAspect` class to add contextual information to an exception using an aspect, without any need 
to change the actual code. Second, there will be an aspect for reporting and swallowing an exception. By combining these two aspects, you can achieve
better maintainability of your application without making the code unreadable and thus unmanageable.

The sample implements two aspects:

* `AddContextOnExceptionAttribute` is meant to be added to all methods in the assembly using the one-liner `[assembly:AddContextOnException]`. Its role is 
   to add the parameter values of the current value to the `Exception.Data["Context"]` object. Since the aspect is supposed to be applied to all 
   user-writen methods, the aspect will gather the parameter values of all user-writen methods in the call stack, and it will be a great help for 
   debugging. This aspect does not suppress the exception.
   
* `ReportAndSwallowExceptionAttribute` writes the exception to the console (including the data collected by  `AddContextOnExceptionAttribute`) and 
   suppresses the exception. Suppressing exceptions is dangerous and you cannot do that on each method. You would typically use this kind of aspects 
   on an event handler (such as WPF and WinForms ones) or a thread entrypoint.

Now you know how to use PostSharp to handle exceptions from one place, with good separation of concerns. The logic you implement this way makes your code much more maintainable and easier to read.

## Limitations

This example is not concerned with many details you are concerned with in your production code:

* Not thread-safe.
* Not optimized.
* Not having robust logging of exception.
