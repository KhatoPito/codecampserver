using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using July09v31.UI.Helpers.ViewPage;
using July09v31.UI.Helpers.ViewPage.InputBuilders;

namespace July09v31.UI.Views
{
    public class InputBuilderExtender<T> : HtmlHelper<T> where T : class
    {
        private readonly InputSpecificationBuilder _builder;

        public InputBuilderExtender(ViewContext viewContext, IViewDataContainer viewDataContainer,
                                    RouteCollection routeCollection)
            : base(viewContext, viewDataContainer, routeCollection)
        {
            _builder = new InputSpecificationBuilder();
        }


        public IInputSpecificationExpression Input(Expression<Func<T, object>> expr)
        {
            return GetInputSpec(expr);
        }

        public IInputSpecificationExpression Input(Expression<Func<T, object>> expr,
                                                   IDictionary<string, object> htmlAttributes)
        {
            return GetInputSpec(expr).Attributes(htmlAttributes);
        }

        public IInputSpecificationExpression Input(Expression<Func<T, object>> expr,
                                                   IDictionary<string, object> htmlAttributes,
                                                   object value)
        {
            return GetInputSpec(expr).WithValue(value).Attributes(htmlAttributes);
        }

        private IInputSpecificationExpression GetInputSpec(Expression<Func<T, object>> expr)
        {
            var view = (IViewBase)ViewDataContainer;
            return _builder.InputFor(view, expr);
        }

        public IInputSpecificationExpression Hidden(Expression<Func<T, object>> expr)
        {
            return GetInputSpec(expr).Using<HiddenInputBuilder>();
        }

        public IInputSpecificationExpression Display(Expression<Func<T, object>> expr)
        {
            return GetInputSpec(expr).Using<NoInputBuilder>();
        }
    }
}