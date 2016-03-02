using System;
using System.Reflection;
using System.Resources;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using PostSharp.Reflection.Syntax;

namespace PostSharp.Samples.ValidateResourceString
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [MulticastAttributeUsage(MulticastTargets.Parameter)]
    public sealed class ValidateResourceStringAttribute : ReferentialConstraint
    {
        private readonly string resourceBaseName;

        public ValidateResourceStringAttribute(string resourceBaseName)
        {
            this.resourceBaseName = resourceBaseName;
        }

        public override bool ValidateConstraint(object target)
        {
            // Validate that the attribute has been applied to a parameter of type string.
            ParameterInfo parameter = (ParameterInfo) target;

            if (parameter.ParameterType != typeof (string))
            {
                Message.Write(parameter, SeverityType.Error, "VRN01", "Cannot use [ValidateResourceString] on parameter {0} because it is not of type string.", parameter);
                return false;
            }

            if (!(parameter.Member is MethodBase))
            {
                Message.Write(parameter, SeverityType.Error, "VRN02", "Cannot use [ValidateResourceString] on parameter {0} because the attribute can only be applied to method parameters.", parameter);
                return false;
            }


            // Validate that the current assembly contains a resource of the given name.
            var assembly = this.GetType().Assembly;
            try
            {
                new ResourceManager(this.resourceBaseName, assembly);
            }
            catch (Exception e)
            {
                Message.Write(parameter, SeverityType.Error, "VRN03", "Cannot load a managed resource named \"{1}\" from assembly \"{0}\": {2}", assembly.GetName().Name, this.resourceBaseName, e.Message);
                return false;
            }

            return true;
        }

        public override void ValidateCode(object target, Assembly assembly)
        {
            ParameterInfo parameter = (ParameterInfo)target;
            ResourceManager resourceManager = new ResourceManager(this.resourceBaseName, assembly);
            Visitor visitor = new Visitor(parameter, resourceManager);

            ISyntaxReflectionService reflectionService =
                PostSharpEnvironment.CurrentProject.GetService<ISyntaxReflectionService>();

            foreach (var usage in ReflectionSearch.GetMethodsUsingDeclaration(parameter.Member))
            {
                visitor.VisitMethodBody(reflectionService.GetMethodBody(usage.UsingMethod,
                    SyntaxAbstractionLevel.ExpressionTree));
            }
        }

        class Visitor : SyntaxTreeVisitor
        {
            private readonly ParameterInfo parameter;
            private readonly ResourceManager resourceManager;

            public Visitor(ParameterInfo parameter, ResourceManager resourceManager)
            {
                this.resourceManager = resourceManager;
                this.parameter = parameter;
            }

            public override object VisitMethodCallExpression(IMethodCallExpression expression)
            {
                if (expression.Method == this.parameter.Member)
                {
                    // We are inspecting the call to the method of interest.    
                    IExpression argument = expression.Arguments[this.parameter.Position];
                    IConstantExpression constantExpression = argument as IConstantExpression;

                    if (constantExpression != null)
                    {
                        var resourceName = (string) constantExpression.Value;
                        if (resourceManager.GetString(resourceName) == null)
                        {
                            Message.Write(expression.ParentMethodBody.Method, SeverityType.Warning, "VRN04", "The string \"{0}\" in method {1} is not a valid resource name.", resourceName, expression.Method );

                        }
                    }
                }

                return base.VisitMethodCallExpression(expression);
            }
        }
    }
}
