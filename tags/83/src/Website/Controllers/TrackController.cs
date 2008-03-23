﻿using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class TrackController : ApplicationController
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IConferenceService _conferenceService;
        private readonly IClock _clock;

        public TrackController(ITrackRepository trackRepository, IConferenceService conferenceService, IClock clock,
                               IAuthorizationService authorizationService)
            : base(authorizationService)
        {
            _trackRepository = trackRepository;
            _conferenceService = conferenceService;
            _clock = clock;
        }

        public void List(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            Track[] tracks = _trackRepository.GetTracksFor(conference);
            ViewData.Add(new ScheduledConference(conference, _clock));
            ViewData.Add(tracks);

            RenderView("List", ViewData);
        }
    }
}