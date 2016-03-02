# PostSharp.Samples.ValidateResourceName

This example demonstrates how to use PostSharp Architecture Framework to validate, at build time,
that the value passed to a parameter is valid string in a resource name.

The constraint is implemented by the `ValidateResourceNameAttribute` class. It is derived from `ReferentialConstraint`
because we need to validate the *usage* of the method, not the method itself. 

The method `ValidateConstraint` validates that the `[ValidateResourceName]` attribute is used on a parameter of type `string`, and checks that the resource
exists in the current project.

The method `ValidateCode` validates the values assigned to this parameter. We first use `ReflectionSearch.GetMethodsUsingDeclaration` to get a list of 
all methods invoking the validated method. Then, we use a `SyntaxTreeVisitor` to analyze the body of these methods. 

