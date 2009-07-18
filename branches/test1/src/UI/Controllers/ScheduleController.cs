using System.Web.Mvc;
using July09v31.Core.Domain.Model;
using July09v31.UI.Helpers.Filters;
using July09v31.UI.Helpers.Mappers;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Controllers
{
    [RequiresConferenceFilter]
    public class ScheduleController : SmartController
    {
        private readonly IScheduleMapper _mapper;

        public ScheduleController(IScheduleMapper mapper)
        {
            _mapper = mapper;
        }

        public ViewResult Index(Conference conference)
        {
            //leverage the schedule mapper to map the conference
            //into our presentation model
            ScheduleForm[] scheduleForms = _mapper.Map(conference);
            return View(scheduleForms);
        }
    }
}