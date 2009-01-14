using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class SessionController : SaveController<Session, SessionForm>
	{
		private readonly ISessionRepository _repository;
		private readonly ISessionMapper _mapper;

		public SessionController(ISessionRepository repository, ISessionMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public ViewResult New()
		{
			return View("Edit", new SessionForm());
		}

		public ViewResult Index(Session session)
		{
			return View(_mapper.Map(session));
		}

		public ViewResult List(Conference conference)
		{
			Session[] sessions = _repository.GetAllForConference(conference);
			return View(_mapper.Map(sessions));
		}

		public ViewResult Edit(Session session)
		{
			return View(_mapper.Map(session));
		}

		[ValidateModel(typeof (SessionForm))]
		public ActionResult Save([Bind(Prefix = "")] SessionForm form)
		{
			return ProcessSave(form, () => RedirectToIndex(form.Conference));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(SessionForm form)
		{
			var result = new ValidationResult();
			if (KeyAlreadyExists(form))
			{
				result.AddError<SessionForm>(x => x.Key, "This session key already exists");
			}
			return result.GetAllErrors();
		}

		private bool KeyAlreadyExists(SessionForm message)
		{			
			Session session = _repository.GetByKey(message.Key);
			return session != null && session.Id != message.Id;
		}

		public RedirectToRouteResult Delete(Session session)
		{
			_repository.Delete(session);
			return RedirectToIndex(session.Conference);
		}

		private RedirectToRouteResult RedirectToIndex(Conference conference)
		{
			return RedirectToAction<SessionController>(x => x.Index(null), new {conference = conference.Key});
		}
	}
}