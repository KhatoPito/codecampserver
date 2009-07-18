using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace July09v31.UI
{
    public static class UrlHelperExtensions
    {
        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression)
        {
            var controllerName = typeof(TController).GetControllerName();
            var actionName = actionExpression.GetActionName();

            return urlHelper.Action(actionName, controllerName);
        }

        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression, object values)
        {
            var controllerName = typeof(TController).GetControllerName();
            var actionName = actionExpression.GetActionName();

            return urlHelper.Action(actionName, controllerName, values);
        }
    }
}