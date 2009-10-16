using System;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class SponsorController : SaveController<UserGroup, SponsorInput>
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupSponsorMapper _mapper;
		private readonly ISecurityContext _securityContext;

		public SponsorController(IUserGroupRepository repository, 
								 IUserGroupSponsorMapper mapper,
		                         ISecurityContext securityContext)
			: base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
		}


		public ActionResult Index(UserGroup usergroup)
		{
			Sponsor[] entities = usergroup.GetSponsors();


			SponsorInput[] entityListDto = _mapper.Map(entities);

			return View(entityListDto);
		}

		public ActionResult New(UserGroup userGroup)
		{
			return View("edit", new SponsorInput());
		}

		[ValidateModel(typeof (SponsorInput))]
		public ActionResult Save(UserGroup userGroup, SponsorInput sponsorInput)
		{
			sponsorInput.ParentID = userGroup.Id;
			return ProcessSave(sponsorInput, entity => RedirectToAction<SponsorController>(s => s.Index(null)));
		}

		public ActionResult Delete(UserGroup userGroup, Sponsor sponsor)
		{
			userGroup.Remove(sponsor);
			_repository.Save(userGroup);
			return RedirectToAction<SponsorController>(c => c.Index(null));
		}


		public ActionResult Edit(UserGroup userGroup, Guid sponsorID)
		{
			Sponsor sponsor = userGroup.GetSponsors().Where(sponsor1 => sponsor1.Id == sponsorID).FirstOrDefault();

			return View(_mapper.Map(sponsor));
		}

		public ActionResult List(UserGroup userGroup)
		{
			Sponsor[] entities = userGroup.GetSponsors();

			SponsorInput[] entityListDto = _mapper.Map(entities);

			return View("HomePageWidget", entityListDto);
		}
	}
}