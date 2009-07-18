using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using July09v31.Core.Domain.Model;
using July09v31.DependencyResolution;
using July09v31.UI.Helpers.Binders;
using July09v31.UI.Views;

namespace July09v31.UI
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
            AutoMapperConfiguration.Configure();
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());

            ModelBinders.Binders.DefaultBinder = new SmartBinder();
            DependencyRegistrar.EnsureDependenciesRegistered();
            ModelBinders.Binders.Add(typeof(UserGroup), DependencyRegistrar.Resolve<UserGroupModelBinder>());
        }
    }
}