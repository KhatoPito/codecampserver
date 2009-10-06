using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using CodeCampServer.Core.Services.Impl;
using CommandProcessor;
using MvcContrib;
using CodeCampServer.UI;
using CodeCampServer.Core.Services;


namespace CodeCampServer.UI.Controllers
{
	public class UserGroupController : SaveController<UserGroup, UserGroupInput>
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupMapper _mapper;
        private readonly ISecurityContext _securityContext;
		private readonly IRulesEngine _rulesEngine;

		public UserGroupController(IUserGroupRepository repository, IUserGroupMapper mapper,IConferenceRepository conferenceRepository,IConferenceMapper conferenceMapper, ISecurityContext securityContext,IRulesEngine rulesEngine) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
	        _securityContext = securityContext;
	    	_rulesEngine = rulesEngine;
		}

		
		public ActionResult Index(UserGroup usergroup)
		{

            UserGroupInput input = _mapper.Map(usergroup);
			return View(input);
		}

		public ActionResult List()
		{
			UserGroup[] entities = _repository.GetAll();

			if (entities.Length < 1)
			{
				return RedirectToAction<UserGroupController>(c => c.New());
			}

			UserGroupInput[] entityListDto = _mapper.Map(entities);
			return View(entityListDto);
		}
		[AcceptVerbs(HttpVerbs.Get)]
        [RequireAuthenticationFilter]
		public ActionResult Edit(Guid Id)
		{

			if (Id == Guid.Empty)
			{
				TempData.Add("message", "UserGroup has been deleted.");
				return RedirectToAction<UserGroupController>(c => c.List());
			}

            UserGroup userGroup = _repository.GetById(Id);
            if(!CurrentUserHasPermissionToEditUserGroup(userGroup))
            {
                return View(ViewPages.NotAuthorized);
            }
            return View(_mapper.Map(userGroup));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		[ValidateModel(typeof(UserGroupInput))]
		public ActionResult Edit(UserGroupInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.Id))
			{
				return View(ViewPages.NotAuthorized);
			}

			if (ModelState.IsValid)
			{
				var result = _rulesEngine.Process(input);
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

	    [ValidateInput(false)] 
		[ValidateModel(typeof (UserGroupInput))]
		public ActionResult Save(UserGroupInput input)
		{
            if(_securityContext.HasPermissionsForUserGroup(input.Id))
            {
                return ProcessSave(input, entity => RedirectToAction<UserGroupController>(c => c.List()));
            }
	        return View(ViewPages.NotAuthorized);
		}
        

		protected override IDictionary<string, string[]> GetFormValidationErrors(UserGroupInput input)
		{
			var result = new ValidationResult();
			if (CurrentUserHasPermissionToEditUserGroup(input.Id) && UserGroupKeyAlreadyExists(input))
			{
				result.AddError<UserGroupInput>(x => x.Key, "This entity key already exists");
			}
			return result.GetAllErrors();
		}

	    private bool UserGroupKeyAlreadyExists(UserGroupInput message)
		{
			UserGroup entity = _repository.GetByKey(message.Key);
			return entity != null && entity.Id != message.Id;
		}

		
        [RequireAdminAuthorizationFilter]
		public ActionResult New()
		{
			return View("Edit", _mapper.Map(new UserGroup()));
		}

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



