
This example demonstrates how to build an aspect `NormalizeStringAttribute` that, when you apply it to any `string` field or property,
will first "normalize" the string (e.g. trim it and change to lower case) before assigning it.

The example is a very simple use of `LocationInterceptionAspect`.