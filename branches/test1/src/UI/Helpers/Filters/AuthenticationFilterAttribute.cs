using System.Web.Mvc;
using July09v31.Core.Domain.Model;
using July09v31.Core.Services;
using July09v31.DependencyResolution;
using MvcContrib;

namespace July09v31.UI.Helpers.Filters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserSession _session;

        public AuthenticationFilterAttribute(IUserSession session)
        {
            _session = session;
        }

        public AuthenticationFilterAttribute()
            : this(DependencyRegistrar.Resolve<IUserSession>()) { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ControllerBase controller = filterContext.Controller;
            User user = _session.GetCurrentUser();
            if (user != null)
            {
                controller.ViewData.Add(user);
            }
        }
    }
}