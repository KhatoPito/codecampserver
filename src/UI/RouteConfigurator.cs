using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI
{
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;

			routes.MapRoute("speaker", "{conferenceKey}/speakers/{speakerKey}", new { controller = "Speaker", action = "index" });

			routes.MapRoute("session", "{conferenceKey}/sessions/{sessionKey}", new { controller = "Session", action = "index" });

			routes.MapRoute("conferenceDefault", "{conferenceKey}/{controller}/{action}",
							new { controller = "conference", action = "index" },									//"schedule|session|timeslot|track|attendee|conference|speaker|admin"
			                new {conferenceKey = new ConferenceKeyCannotBeAControllerNameContraint(),controller="schedule|session|timeslot|track|attendee|conference|speaker|admin"});

	
			routes.MapRoute("default", "{controller}/{action}", new {controller = "home", action = "index"},new{controller="(admin|login|speaker|home|conference)"});
		}
	}
}