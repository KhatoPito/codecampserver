using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI
{
	public class GlobalApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			new RouteConfigurator().RegisterRoutes();
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
		}
	}
}