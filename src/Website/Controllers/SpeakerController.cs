﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Models.Speaker;
using System.Collections.Generic;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Exceptions;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
    public class SpeakerController : Controller
    {
        private readonly IConferenceService _conferenceService;
        private IClock _clock;

        public SpeakerController(IConferenceService conferenceService, IClock clock)
        {            
            _conferenceService = conferenceService;
            _clock = clock;
        }

        [ControllerAction]
        public void Index(string conferenceKey)
        {
            RedirectToAction( new
            {
                Controller="conference", Action="details", conferenceKey=conferenceKey
            });            
        }

        [ControllerAction]
        public void List(string conferenceKey, int? page, int? perPage)
        {
            int effectivePage = page.GetValueOrDefault(0);
            int effectivePerPage = perPage.GetValueOrDefault(20);

            Conference conference = _conferenceService.GetConference(conferenceKey);
            ScheduledConference scheduledConference = new ScheduledConference(conference, _clock);
            IEnumerable<Speaker> speakers = _conferenceService.GetSpeakers(conference, effectivePage, effectivePerPage);
            IEnumerable<SpeakerListing> speakerListings = new SpeakerListingCollection(speakers);
            
            ListSpeakersViewData viewData = new ListSpeakersViewData(scheduledConference, speakerListings, effectivePage, effectivePerPage);

            RenderView("List", viewData);
        }

        [ControllerAction]
        public void View(string conferenceKey, string speakerId)
        {
            Speaker speaker = _conferenceService.GetSpeakerByDisplayName(speakerId);
            RenderView("view", speaker);
        }

        [ControllerAction]
        public void Edit()
        {
            Speaker speaker = _conferenceService.GetLoggedInSpeaker();
            if (speaker != null)
            {                
                RenderView("edit", speaker);
            }
            else
            {
                RedirectToAction(new { Controller="login" });
            }
        }

        [ControllerAction]
        public void Save(string conferenceKey, string displayName, string firstName, string lastName, string website, string comment, string profile, string avatarUrl)
        {
            string email = _conferenceService.GetLoggedInUsername();
            try
            {
                Speaker savedSpeaker = _conferenceService.SaveSpeaker(email, firstName, lastName, website, comment, displayName, profile, avatarUrl);
                
                //TempData["message"] = "Profile saved";
                RedirectToAction(new { Action = "view", conferenceKey = conferenceKey, speakerId = displayName });
            }
            catch (DataValidationException ex)
            {
                //TempData["error"] = ex.Message;
                RedirectToAction("edit");
            }
            
        }
    }
}
