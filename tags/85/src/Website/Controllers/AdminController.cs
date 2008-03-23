﻿using CodeCampServer.Model;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{    
    [AuthorizationFilter(AllowRoles="Administrators,Organizers", Order=1)]
    public class AdminController : ApplicationController
    {
        private IConferenceService _conferenceService;        

        public AdminController(IAuthorizationService authorizationService, IConferenceService conferenceService) : base(authorizationService)
        {
            _conferenceService = conferenceService;
        }
        
        public void Index()
        {
            RenderView("Index");            
        }
        
        public void Schedule()
        {                          
            RenderView("Schedule");
        }
    }
}
