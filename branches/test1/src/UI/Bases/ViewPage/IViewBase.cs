using System.Web.Mvc;

namespace July09v31.UI.Helpers.ViewPage
{
    public interface IViewBase : IViewDataContainer
    {
        IDisplayErrorMessages Errors { get; }
        IInputBuilderFactory InputBuilderFactory { get; }
        HtmlHelper Html { get; }
        UrlHelper Url { get; }
    }
}