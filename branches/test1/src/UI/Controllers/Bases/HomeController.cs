using System.Web.Mvc;
using AutoMapper;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Helpers.Filters;
using July09v31.UI.Helpers.Mappers;
using July09v31.UI.Models;
using July09v31.UI.Models.Forms;
using MvcContrib;

namespace July09v31.UI.Controllers
{
    [AdminUserCreatedFilter]
    public class HomeController : SmartController
    {
        private readonly IUserGroupMapper _mapper;

        public HomeController(IUserGroupMapper mapper)
        {
            _mapper = mapper;
        }

        public ViewResult Index(UserGroup userGroup, IConferenceRepository _conferenceRepository)
        {
            Conference[] conferences = _conferenceRepository.GetFutureForUserGroup(userGroup);

            var conferenceForms =
                (ConferenceForm[])Mapper.Map(conferences, typeof(Conference[]), typeof(ConferenceForm[]));

            ViewData.Add(conferenceForms);


            return View(_mapper.Map(userGroup));
        }
    }
}