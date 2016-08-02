# PostSharp.Samples

## List of examples

| Example                                   | Description                                               | Demonstrated PostSharp features                                                           |
| :---------------------------------------- | :-------------------------------------------------------- | :--------------------------------------------------------------------------------------- |
| PostSharp.Samples.CustomLogging           | Logs method calls including parameter values.             | Simple features of OnMethodBoundaryAspect, LocationInterceptionAspect.                   |
| PostSharp.Samples.CustomCaching           | Caches the results of methods calls                       | OnMethodBoundaryAspect: FlowBehavior, MethodExecutionTag.                                |
| PostSharp.Samples.ExceptionHandling       | Add parameter values to call stack in exception details. Report and then swallow exceptions in entry points.  | OnExceptionAspect including FlowBehavior.                                                |
| PostSharp.Samples.AutoRetry               | Automatically retries a method call when it fails.        | MethodInterceptionAspect                                                                 |
| PostSharp.Samples.WeakEvent               | Prevents memory leaks due to events.                      | EventInterceptionAspect, IInstanceScopedAspect, InstanceLevelAspect, IntroduceInterface  |
| PostSharp.Samples.ValidateResourceName    | Prints a build-time warning when incorrect resource string name is passed to parameter.      | ReferentialConstraint, ReflectionSearch, SyntaxTreeVisitor                               |
| PostSharp.Samples.SessionState            | Stores a field or property in the session state or page view state.       | LocationInterceptionAspect, IInstanceScopedAspect, ImportMember                          |
| PostSharp.Samples.Xaml                    | Demonstrates a few ready-made aspects that are useful for XAML | NotifyPropertyChanged, Recordable, Code Contracts                                        |
| PostSharp.Samples.Transactions            | Automatically executes a method inside a transaction. | OnMethodBoundaryAspect : MethodExecutionTag                                              |
| PostSharp.Samples.Profiling               | Measure different execution times of methods, including async methods. | OnMethodBoundaryAspect : async methods, MethodExecutionTag                               
| PostSharp.Samples.Encryption              | Automatically encrypts and decripts parameter and fields/properties | IAspectProvider, MethodInterceptionAspect, IAdviceProvider, field imports |
| PostSharp.Samples.Threading.PingPong      | The classic educational ping-pong example.                  | Actor |
| PostSharp.samples.Threading.ThreadDispatching | A simple WPF progress bar updated from a background thread. | Background, Dispatched |

## How to use these examples

* To **browse online**, use the samples browser at <http://samples.postsharp.net/>.
* To **download**, go to <https://www.github.com/postsharp/PostSharp.Samples>.

## Public Domain

This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org/>


 