using System;
using System.Linq.Expressions;
using System.Reflection;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;
using MvcContrib.UI.InputBuilder.Helpers;

namespace UITestHelper
{
    public class ScriptWrittenInputWrapperFactory : IInputWrapperFactory {
        public bool CanHandle(PropertyInfo propertyInfo)
        {
            return propertyInfo.AttributeExists<MultilineAttribute>();
        }

        public IInputWrapper Create(LambdaExpression expression, string text)
        {
            return new ScriptWrittenTextBoxInputWrapper(ReflectionHelper.BuildNameFrom(expression),text);
        }
    }
}