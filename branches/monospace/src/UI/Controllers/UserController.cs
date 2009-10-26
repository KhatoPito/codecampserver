using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserMapper _mapper;
		private readonly IUserRepository _repository;
		private readonly ISecurityContext _securityContext;
		private readonly IUserSession _session;
		
		public UserController(IUserRepository repository, IUserMapper mapper, ISecurityContext securityContext,
							  IUserSession session)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
			_session = session;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Edit(User user)
		{
			if (user == null)
			{
				return View(_mapper.Map(_session.GetCurrentUser()));
			}

			if (!_securityContext.IsAdmin())
			{
				return View("NotAuthorized");
			}
			UserInput input = _mapper.Map(user);
			return View(input);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(false)]
		[ValidateModel(typeof (UserInput))]
		public ActionResult Edit(UserInput input)
		{
			return View();
		}



		public ViewResult Index()
		{
			ViewData.Add(_mapper.Map(_repository.GetAll()));
			return View();
		}
	}
}