using System.Web.Mvc;
using July09v31.Core.Domain;
using July09v31.DependencyResolution;
using July09v31.UI.Controllers;

namespace July09v31.UI.Helpers.Filters
{
    public class AdminUserCreatedFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserRepository _repository;

        public AdminUserCreatedFilterAttribute(IUserRepository repository)
        {
            _repository = repository;
        }

        public AdminUserCreatedFilterAttribute() : this(DependencyRegistrar.Resolve<IUserRepository>()) { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int userCount = _repository.GetAll().Length;
            if (userCount == 0)
            {
                RedirectToAdminNew(filterContext);
            }
        }

        public virtual void RedirectToAdminNew(ActionExecutingContext context)
        {
            var urlhelper = new UrlHelper(context.RequestContext);
            string url = urlhelper.Action<UserController>(c => c.Edit(null));
            context.Result = new RedirectResult(url);
        }
    }
}