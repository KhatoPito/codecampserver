using System.Web.Mvc;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class MeetingController : SmartController
	{
		private readonly IMeetingRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly ISecurityContext _securityContext;

		public MeetingController(IMeetingRepository repository, IMappingEngine mappingEngine, ISecurityContext securityContext)
		{
			_repository = repository;
			_mapper = mappingEngine;
			_securityContext = securityContext;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Edit(Meeting meeting)
		{
			var input = new MeetingInput();
			if (meeting == null)
			{
				_mapper.Map(new Meeting(), input);
				return View(input);
			}

			_mapper.Map(meeting, input);
			return View(input);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(false)]
		[ValidateModel(typeof (MeetingInput))]
		public ActionResult Edit(MeetingInput input)
		{

			if (!ModelState.IsValid)
			{
				return View(input);
			}

			Meeting meeting = _mapper.Map<MeetingInput, Meeting>(input);
			_repository.Save(meeting);
			return RedirectToAction<HomeController>(c => c.Index());
		}


		public ActionResult New()
		{
			return RedirectToAction<MeetingController>(c => c.Edit((Meeting)null));
		}
	}
}