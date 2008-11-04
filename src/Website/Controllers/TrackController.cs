using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using MvcContrib;

namespace CodeCampServer.Website.Controllers
{
    [Authorize(Roles="Administrator")]
	public class TrackController : BaseController
	{
	    private readonly ITrackService _trackService;		
		private readonly IConferenceRepository _conferenceRepository;

		public TrackController(IConferenceRepository conferenceRepository, IUserSession userSession, 
            ITrackService trackService) : base(userSession)
		{
		    _trackService = trackService;
		    _conferenceRepository = conferenceRepository;
		}

		public ActionResult List(string conferenceKey)
		{
			var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
			var tracks = _trackService.GetTracksFor(conference);
			ViewData.Add(conference);
			ViewData.Add(tracks);

			return View();
		}

        public ActionResult New(string conferenceKey)
        {
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            ViewData.Add(conference);
            ViewData.Add(new Track());
            return View("edit");
        }        

        public ActionResult Edit(string conferenceKey, string trackName)
        {
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var track = _trackService.FindTrack(conference, trackName);

            ViewData.Add(conference);
            ViewData.Add(track);
            return View();
        }
	}
}