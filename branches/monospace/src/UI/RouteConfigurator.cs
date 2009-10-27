using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.Routing;

namespace CodeCampServer.UI
{
	public class RouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;

			routes.Clear();

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{resource}.gif/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
			routes.IgnoreRoute("{resource}.css/{*pathInfo}");
			routes.IgnoreRoute("{resource}.jpg/{*pathInfo}");
			routes.IgnoreRoute("{resource}.png/{*pathInfo}");
			routes.IgnoreRoute("{resource}.js/{*pathInfo}");

			MvcRoute.MappUrl("{controller}/{action}")
				.WithDefaults(new {controller = "Home", action = "Index"})
				.AddWithName("default", routes);
		}
	}
}