using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Helpers.Binders;
using CodeCampServer.UI.Views;

namespace CodeCampServer.UI
{
	public class GlobalApplication : HttpApplication
	{
		protected void Application_Start()
		{
		}
	}
}