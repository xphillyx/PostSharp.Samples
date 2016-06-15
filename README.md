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

## How to use these examples

* To **browse online**, use the samples browser at <http://samples.postsharp.net/>.
* To **download**, go to <https://www.github.com/postsharp/PostSharp.Samples>.



 