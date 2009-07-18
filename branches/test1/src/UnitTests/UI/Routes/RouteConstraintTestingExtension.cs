using System.Web.Routing;

namespace July09v31.UnitTests.UI.Routes
{
    public static class RouteConstraintTestingExtension
    {
        public static bool Match(this IRouteConstraint constraint, string parameterName, string value)
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add(parameterName, value);
            return constraint.Match(null, null, parameterName, routeValueDictionary, RouteDirection.IncomingRequest);
        }
    }
}