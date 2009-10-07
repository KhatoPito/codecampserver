using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using CommandProcessor;
using Tarantino.RulesEngine;

namespace CodeCampServer.UI.Controllers
{
	public class UserGroupController : SmartController
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupMapper _mapper;
		private readonly ISecurityContext _securityContext;
		private readonly IRulesEngine _rulesEngine;

		public UserGroupController(IUserGroupRepository repository, IUserGroupMapper mapper,
		                           IConferenceRepository conferenceRepository, IConferenceMapper conferenceMapper,
		                           ISecurityContext securityContext, IRulesEngine rulesEngine) 
			//: base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
			_rulesEngine = rulesEngine;
		}


		public ActionResult Index(UserGroup usergroup)
		{
			return View(_mapper.Map(usergroup));
		}

		public ActionResult List()
		{
			UserGroup[] entities = _repository.GetAll();
			return View(_mapper.Map(entities));
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[RequireAuthenticationFilter]
		public ActionResult Edit(UserGroup entityToEdit)
		{
			if (entityToEdit == null)
			{
				entityToEdit = new UserGroup();
			}
			else
			{
				if (!CurrentUserHasPermissionToEditUserGroup(entityToEdit))
				{
					return View(ViewPages.NotAuthorized);
				}
			}
			return View(_mapper.Map(entityToEdit));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		[ValidateModel(typeof (UserGroupInput))]
		public ActionResult Edit(UserGroupInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.Id))
			{
				return View(ViewPages.NotAuthorized);
			}

			if (ModelState.IsValid)
			{
				ExecutionResult result = _rulesEngine.Process(input);
				if (result.Successful)
				{
					var userGroup = result.ReturnItems.Get<UserGroup>();
					return RedirectToAction<HomeController>(c => c.Index(userGroup));
				}
			}
			return View(input);
		}

		protected bool CurrentUserHasPermissionToEditUserGroup(Guid Id)
		{
			return CurrentUserHasPermissionToEditUserGroup(_repository.GetById(Id));
		}

		protected bool CurrentUserHasPermissionToEditUserGroup(UserGroup userGroup)
		{
			return _securityContext.HasPermissionsFor(userGroup);
		}



		//protected override IDictionary<string, string[]> GetFormValidationErrors(UserGroupInput input)
		//{
		//    var result = new ValidationResult();
		//    if (CurrentUserHasPermissionToEditUserGroup(input.Id) && UserGroupKeyAlreadyExists(input))
		//    {
		//        result.AddError<UserGroupInput>(x => x.Key, "This entity key already exists");
		//    }
		//    return result.GetAllErrors();
		//}

		//private bool UserGroupKeyAlreadyExists(UserGroupInput message)
		//{
		//    UserGroup entity = _repository.GetByKey(message.Key);
		//    return entity != null && entity.Id != message.Id;
		//}


		//[RequireAdminAuthorizationFilter]
		//public ActionResult New()
		//{
		//    return View("Edit", _mapper.Map(new UserGroup()));
		//}

		[RequireAdminAuthorizationFilter]
		public ActionResult Delete(UserGroup entity)
		{
			if (!CurrentUserHasPermissionToEditUserGroup(entity))
			{
				return View(ViewPages.NotAuthorized);
			}

			if (entity.GetUsers().Length == 0)
			{
				_repository.Delete(entity);
			}
			else
			{
				TempData.Add("message", "UserGroup cannot be deleted.");
			}

			return RedirectToAction<UserGroupController>(c => c.List());
		}
	}
}