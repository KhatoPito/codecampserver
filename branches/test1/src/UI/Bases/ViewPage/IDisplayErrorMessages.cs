using System.Web.Mvc;

namespace July09v31.UI.Helpers.ViewPage
{
    public interface IDisplayErrorMessages
    {
        string Display();
        ModelStateDictionary ModelState { set; }
        TempDataDictionary TempData { set; }
    }
}