using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Controllers
{
	public class EventController : SmartController
	{
		private const string ANNOUNCEMENT_PARTIAL_SUFFIX = "Announcement";
		private IEventRepository _eventRepository;
		public EventController(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public ViewResult Announcement(Event @event)
		{
			string typeName = @event.GetType().Name;
			return View(typeName + ANNOUNCEMENT_PARTIAL_SUFFIX);
		}

		public ViewResult UpComing(UserGroup group)
		{
			Event[] events = _eventRepository.GetFutureForUserGroup(group);
			return List(events);
		}

		private ViewResult List(Event[] events)
		{
			return View(events);
		}
	}
}