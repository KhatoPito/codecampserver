using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI
{
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;
			routes.MapRoute("admin", "admin/{action}", new { controller = "admin", action = "index" });
			routes.MapRoute("tracks", "{conferenceKey}/track/{action}", new {controller = "track", action = "index"});
			routes.MapRoute("sessions", "{conferenceKey}/sessions/{action}", new { controller = "session", action = "index" });
			routes.MapRoute("conferenceDefault", "{conferenceKey}/{controller}/{action}", new { controller = "home", action = "index" });
			routes.MapRoute("default", "{controller}/{action}", new { controller = "home", action = "index" });
//			routes.MapRoute("login", "login/{action}", new {controller = "login", action = "index"});
//			routes.MapRoute("new-conference", "conference/new", new {controller = "conference", action = "new"});
//			routes.MapRoute("speaker", "{conferenceKey}/speaker/{speakerId}", new {controller = "speaker", action = "view"});
//			routes.MapRoute("speakers", "{conferenceKey}/speakers/{action}", new {controller = "speaker", action = "list"});
//			routes.MapRoute("schedule", "{conferenceKey}/schedule/{action}", new {controller = "schedule", action = "index"});
//			routes.MapRoute("sessions", "{conferenceKey}/sessions/{action}", new {controller = "session", action = "list"});
//			routes.MapRoute("sponsors", "{conferenceKey}/sponsors/{action}", new {controller = "sponsor", action = "list"});
//			routes.MapRoute("sponsor-edit", "{conferenceKey}/sponsors/{action}/{sponsorName}",
//			                new {controller = "sponsor", action = "edit"});
//			routes.MapRoute("conference", "{conferenceKey}/{action}", new {controller = "conference", action = "infde"});
//			routes.MapRoute("default_aspx", "Default.aspx", new {controller = "conference", action = "index"});
		}
	}
}
