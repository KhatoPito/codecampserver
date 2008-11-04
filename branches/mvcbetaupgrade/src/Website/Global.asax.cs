using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Helpers;
using CodeCampServer.Website.Impl;
using CodeCampServer.Website.Security;
using MvcContrib.Castle;

namespace CodeCampServer.Website
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			Log.EnsureInitialized();
			RegisterMvcTypes();
		    RegisterComponents();

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.GetContainer()));
            ControllerBuilder.Current.DefaultNamespaces.Add("CodeCampServer.Website.Controllers");
			
			setupRoutes();
		}

	    private void RegisterComponents()
	    {
	        IoC.Register<IClock, SystemClock>();
            IoC.Register<IHttpContextProvider, HttpContextProvider>();    
            IoC.Register<ISessionBuilder, HybridSessionBuilder>();
            IoC.Register<ICryptographer, Cryptographer>();
            IoC.Register<IUserSession, UserSession>();
            IoC.Register<IAuthenticator, Authenticator>();
            IoC.Register<IConferenceService, ConferenceService>();
            IoC.Register<ITrackService, TrackService>();
           
		    IoC.Register<IConferenceRepository, ConferenceRepository>();
		    IoC.Register<IPersonRepository, PersonRepository>();
		    IoC.Register<ISessionRepository, SessionRepository>();
		    IoC.Register<ITimeSlotRepository, TimeSlotRepository>();
		    IoC.Register<ITrackRepository, TrackRepository>();
	    }

	    protected void Application_BeginRequest(Object sender, EventArgs e)
        {            
            //take out any .mvc extension so we don't need two sets of routes
            var app = sender as HttpApplication;
            if (app != null)
            {
                string url = app.Request.Url.PathAndQuery;
                if(url.Contains(".mvc"))
                    app.Context.RewritePath(url.Replace(".mvc", ""));                
            }
        }

		public static void RegisterMvcTypes()
		{
			IoC.Register<IRouteConfigurator, RouteConfigurator>();
		    IoC.RegisterControllers(typeof(Global).Assembly);
		}

		private static void setupRoutes()
		{
			var configurator = IoC.Resolve<IRouteConfigurator>();
			configurator.RegisterRoutes();
		}
	}
}