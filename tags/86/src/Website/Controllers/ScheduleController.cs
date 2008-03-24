using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IConferenceService _conferenceService;
        private readonly IClock _clock;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public ScheduleController(IConferenceService conferenceService,
                                  IClock clock,
                                  ITimeSlotRepository timeSlotRepository,
                                  IAuthorizationService authorizationService)
            : base(authorizationService)
        {
            _conferenceService = conferenceService;
            _clock = clock;
            _timeSlotRepository = timeSlotRepository;
        }

        public void Index(string conferenceKey)
        {
            var conference = _conferenceService.GetConference(conferenceKey);
            var schedule = new Schedule(conference, _clock, _timeSlotRepository);
            ViewData.Add(schedule);
            RenderView("View");
        }
    }
}