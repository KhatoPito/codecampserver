using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using CodeCampServer.Model.Security;
using System.Web.Routing;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class ConferenceControllerTester
	{
		private MockRepository _mocks;
		private IConferenceService _service;
	    private IAuthorizationService _authService;
		private Conference _conference;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_service = _mocks.CreateMock<IConferenceService>();
		    _authService = _mocks.DynamicMock<IAuthorizationService>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}


		private class TestingConferenceController : ConferenceController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;
            public RouteValueDictionary ActualRedirectToActionValues;

            public event EventHandler RedirectedToAction;

			public TestingConferenceController(IConferenceService conferenceService, IAuthorizationService authService, IClock clock)
                : base(conferenceService, authService, clock)
			{
			}

			protected override void RenderView(string viewName,
			                                   string masterName,
			                                   object viewData)
			{
				ActualViewName = viewName;
				ActualMasterName = masterName;
				ActualViewData = viewData;
			}

            protected override void RedirectToAction(RouteValueDictionary values)
            {
                ActualRedirectToActionValues = values;
                if (RedirectedToAction != null)
                    RedirectedToAction(this, null);
            }
		}

        [Test]
        public void IndexShouldRedirectToDetailsAction()
        {
            TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
            controller.Index();

            Assert.AreEqual(controller.ActualRedirectToActionValues["Action"], "details");
        }

        [Test]
        public void ListAsAdminShouldRenderListViewWithAllConferences()
        {
            List<Conference> conferences = new List<Conference>(new Conference[] { _conference });
            SetupResult.For(_service.GetAllConferences())
                .Return(conferences);
            SetupResult.On(_authService)
                .Call(_authService.IsAdministrator)
                .Return(true);
            _mocks.ReplayAll();

            TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
            controller.List();

            Assert.That(controller.ActualViewName, Is.EqualTo("List"));
            Assert.That(controller.ViewData.Get<IEnumerable<Conference>>(), Is.SameAs(conferences));
        }

        [Test]
        public void ListAsNonAdminShouldRedirectToLogin()
        {
            SetupResult.On(_authService)
                .Call(_authService.IsAdministrator)
                .Return(false);
            _mocks.ReplayAll();

            TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());

            // Have the RedirectToAction event mimic ResposeRedirect by throwing 
            // an exception and causing the end of the request.
            ApplicationException exception = new ApplicationException("Redirected!");
            controller.RedirectedToAction += delegate { throw exception; };
            try { controller.List(); }
            catch(ApplicationException x) { Assert.That(x, Is.SameAs(exception)); }

            Assert.AreEqual(controller.ActualRedirectToActionValues["Controller"], "Login");
            Assert.AreEqual(controller.ActualRedirectToActionValues["Action"], "index");
        }


        [Test]
		public void ShouldGetConferenceToShowDetails()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service, _authService, new ClockStub());

			controller.Details("austincodecamp2008");
			Assert.That(controller.ActualViewName, Is.EqualTo("details"));
			Schedule actualViewData = controller.ViewData.Get<Schedule>();
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
		}

		[Test]
		public void ShouldGetConferenceForTheRegistrationForm()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.PleaseRegister("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerform"));
			Schedule actualViewData = controller.ViewData.Get<Schedule>();
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
		}

		[Test]
		public void ShouldRegisterANewAttendee()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008")).Return(_conference);
			Person actualAttendee = new Person();
			Expect.Call(_service.RegisterAttendee("firstname", "lastname",
			                                      "email", "website", "comment", _conference, "password")).Return(actualAttendee);

			_mocks.ReplayAll();

			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());            
			controller.Register("austincodecamp2008", "firstname", "lastname", "email", "website", "comment", "password");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerconfirm"));

			Schedule schedule = controller.ViewData.Get<Schedule>();
			Person viewDataAttendee = controller.ViewData.Get<Person>();

			Assert.That(schedule, Is.Not.Null);
			Assert.That(viewDataAttendee, Is.Not.Null);
			Assert.That(schedule.Conference, Is.EqualTo(_conference));
			Assert.That(viewDataAttendee, Is.EqualTo(actualAttendee));
		}

		[Test]
		public void ShouldListAttendeesForAConference()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
            _conference.AddAttendee(new Person("George", "Carlin", "gcarlin@aol.com"));
            _conference.AddAttendee(new Person("Dave", "Chappelle", "rjames@gmail.com"));
			
			//SetupResult.For(_service.GetAttendees(_conference, 0, 2)).IgnoreArguments().Return(toReturn);
			_mocks.ReplayAll();

			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.ListAttendees("austincodecamp2008", 0, 2);

			Assert.That(controller.ActualViewName, Is.EqualTo("listattendees"));

			AttendeeListing[] attendeeListings = controller.ViewData.Get<AttendeeListing[]>();
			Schedule conference = controller.ViewData.Get<Schedule>();
			Assert.That(attendeeListings, Is.Not.Null);
			Assert.That(conference, Is.Not.Null);
			Assert.That(conference.Conference, Is.EqualTo(_conference));

			Assert.That(attendeeListings.Length, Is.EqualTo(2));
			Assert.That(attendeeListings[0].Name, Is.EqualTo("George Carlin"));
			Assert.That(attendeeListings[1].Name, Is.EqualTo("Dave Chappelle"));
		}

		[Test]
		public void NewActionShouldRenderEditViewWithNewConference()
		{
			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.New();

			Assert.That(controller.ViewData.Contains<Conference>());
			Assert.That(controller.ActualViewName, Is.EqualTo("Edit"));
		}
	}
}
