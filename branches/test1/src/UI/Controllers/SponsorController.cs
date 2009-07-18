using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator.Attributes;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Helpers.Filters;
using July09v31.UI.Helpers.Mappers;
using July09v31.UI.Models.Forms;
using July09v31.Core.Services.Impl;
using MvcContrib;
using July09v31.UI;
using July09v31.Core.Services;


namespace July09v31.UI.Controllers
{
    public class SponsorController : SaveController<UserGroup, SponsorForm>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IUserGroupSponsorMapper _mapper;
        private readonly ISecurityContext _securityContext;

        public SponsorController(IUserGroupRepository repository, IUserGroupSponsorMapper mapper, ISecurityContext securityContext)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _securityContext = securityContext;
        }


        public ActionResult Index(UserGroup usergroup)
        {
            var entities = usergroup.GetSponsors();

            var entityListDto = (SponsorForm[])AutoMapper.Mapper.Map(entities, typeof(Sponsor[]), typeof(SponsorForm[]));

            return View(entityListDto);
        }

        public ActionResult New(UserGroup userGroup)
        {
            return View("edit", new SponsorForm());
        }

        [ValidateModel(typeof(SponsorForm))]
        public ActionResult Save(UserGroup userGroup, SponsorForm sponsorForm)
        {
            sponsorForm.ParentID = userGroup.Id;
            return ProcessSave(sponsorForm, entity => RedirectToAction<SponsorController>(s => s.Index(null)));
        }

        public ActionResult Delete(UserGroup userGroup, Sponsor sponsor)
        {
            userGroup.Remove(sponsor);
            _repository.Save(userGroup);
            return RedirectToAction<SponsorController>(c => c.Index(null));
        }


        public ActionResult Edit(UserGroup userGroup, Guid sponsorID)
        {
            var sponsor = userGroup.GetSponsors().Where(sponsor1 => sponsor1.Id == sponsorID).FirstOrDefault();

            return View(AutoMapper.Mapper.Map<Sponsor, SponsorForm>(sponsor));
        }

        public ActionResult List(UserGroup userGroup)
        {
            var entities = userGroup.GetSponsors();

            var entityListDto = (SponsorForm[])AutoMapper.Mapper.Map(entities, typeof(Sponsor[]), typeof(SponsorForm[]));

            return View("HomePageWidget", entityListDto);
        }
    }
}



