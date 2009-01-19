using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;

namespace CodeCampServer.UI.Controllers
{
	[AdminUserCreatedFilterAttribute]
	[RequiresConferenceFilter]
	public class HomeController : SmartController
	{
		private readonly IConferenceRepository _repository;

		public HomeController(IConferenceRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Index()
		{
			Conference conf = _repository.GetNextConference();
			if (conf != null)
			{
				return RedirectToAction<ConferenceController>(c => c.Index(null), new {conferenceKey = conf.Key});
			}

			return RedirectToAction<LoginController>(c => c.Index());
		}
	}
}