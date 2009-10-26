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

			//MvcRoute.MappUrl("{conferenceKey}/speakers/{speakerKey}")
			//    .WithDefaults(new {controller = "Speaker", action = "index"})
			//    .AddWithName("speaker", routes)
			//    .RouteHandler = new DomainNameRouteHandler();

			//MvcRoute.MappUrl("{conferenceKey}/sessions/{sessionKey}")
			//    .WithDefaults(new {controller = "Session", action = "index"})
			//    .AddWithName("session", routes)
			//    .RouteHandler = new DomainNameRouteHandler();

			//MvcRoute.MappUrl("{conferenceKey}/proposal/vote/{id}")
			//    .WithDefaults(new {controller = "Proposal", action = "vote"})
			//    .AddWithName("ProposalVote", routes)
			//    .RouteHandler = new DomainNameRouteHandler();


			//MvcRoute.MappUrl("{conferenceKey}/{controller}/{action}")
			//    .WithDefaults(new {controller = "Conference", action = "index"})
			//    .WithConstraints(new
			//                        {
			//                            conferenceKey = new ConferenceKeyCannotBeAControllerNameConstraint(),
			//                            controller =
			//                        "schedule|session|timeslot|track|attendee|conference|speaker|admin|proposal|user|sponsor|meeting"
			//                        })
			//    .AddWithName("conferenceDefault", routes)
			//    .RouteHandler = new DomainNameRouteHandler();

			MvcRoute.MappUrl("{controller}/{action}")
				.WithDefaults(new {controller = "Home", action = "Index"})
				.AddWithName("default", routes);
				//.RouteHandler = new DomainNameRouteHandler();
		}
	}
}