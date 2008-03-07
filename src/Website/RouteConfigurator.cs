using System;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Model;
using StructureMap;

namespace CodeCampServer.Website
{
	[Pluggable(Keys.DEFAULT)]
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;

			routes.Add(new Route("login/[action]",
			                     new RouteValueDictionary(new {Controller = "login", Action = "index"}),
			                     new BetterMvcRouteHandler()));

			routes.Add(new Route("[conferenceKey]/speaker/[speakerId]",
								 new RouteValueDictionary(new { Controller = "speaker", Action = "view" }),
								 new BetterMvcRouteHandler()));


            routes.Add(new Route
            {
                Url = "[conferenceKey]/speakers/[action]",
                Defaults = new { Controller = "speaker", Action = "list" },
                RouteHandler = typeof(BetterMvcRouteHandler)
            });

			routes.Add(new Route
			{
				Url = "[conferenceKey]/schedule/[action]",
				Defaults = new { Controller = "schedule", Action = "index" },
				RouteHandler = typeof(BetterMvcRouteHandler)
			});

            routes.Add(new Route
            {
                Url = "[conferenceKey]/sessions/[action]",
                Defaults = new { Controller = "session", Action = "list" },
                RouteHandler = typeof(BetterMvcRouteHandler)
            });

            routes.Add(new Route
            {
                Url = "[conferenceKey]/sponsors/[action]",
                Defaults = new { Controller = "sponsor", Action = "list" },
                RouteHandler = typeof(BetterMvcRouteHandler)
            });

            routes.Add(new Route
            {
                Url = "[conferenceKey]/sponsors/[action]/[sponsorName]",
                Defaults = new { Controller = "sponsor", Action = "edit" },
                RouteHandler = typeof(BetterMvcRouteHandler)
            });

            routes.Add(new Route("[conferenceKey]/[action]",   
				new ControllerDefaults("index", "conference"),
				typeof(BetterMvcRouteHandler)));

			routes.Add(new Route("Default.aspx",
				new
				{
					action = "details",
					controller = "conference",
					conferenceKey = "austincodecamp2008"
				},
				typeof(BetterMvcRouteHandler)));
		}
	}
}
