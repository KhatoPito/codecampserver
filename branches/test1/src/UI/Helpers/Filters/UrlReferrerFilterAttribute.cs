using System.Web.Mvc;
using July09v31.DependencyResolution;
using July09v31.UI.Services;

namespace July09v31.UI.Helpers.Filters
{
    public class UrlReferrerFilterAttribute : ActionFilterAttribute
    {
        public const string UrlReferrer = "UrlReferrer";
        private IUrlService _urlService;
        public UrlReferrerFilterAttribute() : this(DependencyRegistrar.Resolve<IUrlService>()) { }

        public UrlReferrerFilterAttribute(IUrlService urlService)
        {
            _urlService = urlService;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string referrer;
            if (TheReferrerIsInFromTheCurrentForm(filterContext))
            {
                referrer = GetReferrerFromTheCurrentForm(filterContext);
            }
            else
            {
                referrer = GetReferrerFromTheBrowserHeader();
            }
            AddTheReferrerToTheViewData(filterContext, referrer);
        }

        private void AddTheReferrerToTheViewData(ActionExecutingContext filterContext, string referrer)
        {
            filterContext.Controller.ViewData.Add(UrlReferrer, referrer);
        }

        private string GetReferrerFromTheBrowserHeader()
        {
            return _urlService.UrlReferrer;
        }

        private string GetReferrerFromTheCurrentForm(ActionExecutingContext filterContext)
        {
            return filterContext.HttpContext.Request.Form[UrlReferrer];
        }

        private bool TheReferrerIsInFromTheCurrentForm(ActionExecutingContext filterContext)
        {
            return filterContext.HttpContext.Request.Form[UrlReferrer] != null;
        }
    }
}