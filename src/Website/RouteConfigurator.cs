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

            routes.Add(new Route("[conferenceKey]/speakers/[action]",
                                 new RouteValueDictionary(new { Controller = "speaker", Action = "list" }),
                                 new BetterMvcRouteHandler()));

            routes.Add(new Route("[conferenceKey]/schedule/[action]",
                                 new RouteValueDictionary(new { Controller = "schedule", Action = "index" }),
                                 new BetterMvcRouteHandler()));

            routes.Add(new Route("[conferenceKey]/sessions/[action]",
                                 new RouteValueDictionary(new { Controller = "session", Action = "list" }),
                                 new BetterMvcRouteHandler()));

            routes.Add(new Route("[conferenceKey]/sponsors/[action]",
                                 new RouteValueDictionary(new { Controller = "sponsor", Action = "list" }),
                                 new BetterMvcRouteHandler()));

            routes.Add(new Route("[conferenceKey]/sponsors/[action]/[sponsorName]",
                                 new RouteValueDictionary(new { Controller = "sponsor", Action = "edit" }),
                                 new BetterMvcRouteHandler()));

            routes.Add(new Route("[conferenceKey]/[action]",
                                 new RouteValueDictionary(new { Controller = "conference", Action = "index" }),
                                 new BetterMvcRouteHandler()));

            routes.Add(new Route("Default.aspx",
                                 new RouteValueDictionary(new { Controller = "conference", Action = "details", ConferenceKey = "austincodecamp2008" }),
                                 new BetterMvcRouteHandler()));
		}
	}
}
