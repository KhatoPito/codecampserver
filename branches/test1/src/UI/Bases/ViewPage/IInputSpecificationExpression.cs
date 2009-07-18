using System.Collections.Generic;

namespace July09v31.UI.Helpers.ViewPage
{
    public interface IInputSpecificationExpression
    {
        IInputSpecificationExpression Attributes(IDictionary<string, object> attributes);
        IInputSpecificationExpression Using<T>() where T : IInputBuilder;
        IInputSpecificationExpression WithValue(object value);
    }
}