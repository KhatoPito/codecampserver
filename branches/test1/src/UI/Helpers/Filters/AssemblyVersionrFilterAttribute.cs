using System.Web.Mvc;
using July09v31.DependencyResolution;
using July09v31.Infrastructure.UI.Services;
using July09v31.UI.Services;

namespace July09v31.UI.Helpers.Filters
{
    public class AssemblyVersionFilterAttribute : ActionFilterAttribute
    {
        public const string AssemblyVersion = "AssemblyVersion";
        private IAssemblyVersion _assemblyVersion;
        public AssemblyVersionFilterAttribute() : this(DependencyRegistrar.Resolve<IAssemblyVersion>()) { }

        public AssemblyVersionFilterAttribute(IAssemblyVersion assemblyVersionService)
        {
            _assemblyVersion = assemblyVersionService;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string version = _assemblyVersion.GetAssemblyVersion();

            AddTheVersionToTheViewData(filterContext, version);
        }

        private void AddTheVersionToTheViewData(ActionExecutingContext filterContext, string referrer)
        {
            filterContext.Controller.ViewData.Add(AssemblyVersion, referrer);
        }

    }
}