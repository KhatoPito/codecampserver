using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class HomeController : SmartController
	{
		private readonly IMeetingRepository _meetingRepository;
		private readonly IMeetingMapper _meetingMapper;

		public HomeController(IMeetingRepository meetingRepository, IMeetingMapper meetingMapper)
		{
			_meetingRepository = meetingRepository;
			_meetingMapper = meetingMapper;
		}

		public ViewResult Index()
		{
			MeetingAnnouncementDisplay[] meetings = _meetingMapper.Map(_meetingRepository.GetAll());
			return View(new HomeDisplay(){Meetings = meetings});
		}


		public ViewResult About()
		{
			return View();
		}
	}
}