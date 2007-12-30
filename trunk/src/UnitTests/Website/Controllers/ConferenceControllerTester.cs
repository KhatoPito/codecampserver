using System;
using System.Collections;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class ConferenceControllerTester
    {
    	private MockRepository _mocks;
    	private IConferenceService _service;
    	private Conference _conference;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_service = _mocks.CreateMock<IConferenceService>();
			_conference = new Conference("austincodecamp2008",
										 "Austin Code Camp");	
		}

    	[Test]
        public void ScheduleShouldGetConferenceByKeyAndSendScheduleToTheView()
        {
            SetupResult.For(_service.GetConference("austincodecamp2008"))
                .Return(_conference);
            _mocks.ReplayAll();

            TestingConferenceController controller =
                new TestingConferenceController(_service);
            controller.Schedule("austincodecamp2008");

            Assert.That(controller.ActualViewName, Is.EqualTo("showschedule"));
            Schedule actualViewData = controller.ActualViewData as Schedule;
            Assert.That(actualViewData, Is.Not.Null);
            Assert.That(actualViewData.ConferenceName,
                        Is.EqualTo("Austin Code Camp"));
        }

        private class TestingConferenceController : ConferenceController
        {
            public string ActualViewName;
            public string ActualMasterName;
            public object ActualViewData;

        	public TestingConferenceController(IConferenceService conferenceService) : base(conferenceService)
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
        }

    	[Test]
    	public void ShouldGetConferenceToShowDetails()
    	{
    		SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service);
			controller.Details("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("details"));
    		Conference actualViewData = controller.ViewData["conference"] as Conference;
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData, Is.EqualTo(_conference));
    	}

		[Test]
		public void ShouldGetConferenceForTheRegistrationForm()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service);
			controller.PleaseRegister("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerform"));
			Conference actualViewData = controller.ViewData["conference"] as Conference;
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData, Is.EqualTo(_conference));
		}

		[Test]
		public void ShouldRegisterANewAttendee()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_service.RegisterAttendee(null);
			Attendee actualAttendee = new Attendee();
			LastCall.IgnoreArguments().Do(new Action<Attendee>(delegate(Attendee obj) { actualAttendee = obj; }));
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service);
			controller.Register("austincodecamp2008", "a", "b", "c", "d", "e");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerconfirm"));
			Conference viewDataConference = controller.ViewData["conference"] as Conference;
			Attendee viewDataAttendee = controller.ViewData["attendee"] as Attendee;
			Assert.That(viewDataConference, Is.Not.Null);
			Assert.That(viewDataAttendee, Is.Not.Null);
			Assert.That(viewDataConference, Is.EqualTo(_conference));
			Assert.That(viewDataAttendee, Is.EqualTo(actualAttendee));
			Assert.That(viewDataAttendee.Contact.FirstName, Is.EqualTo("a"));
			Assert.That(viewDataAttendee.Contact.LastName, Is.EqualTo("b"));
			Assert.That(viewDataAttendee.Contact.Email, Is.EqualTo("c"));
			Assert.That(viewDataAttendee.Website, Is.EqualTo("d"));
			Assert.That(viewDataAttendee.Comment, Is.EqualTo("e"));
		}

		[Test]
		public void ShouldListAttendeesForAConference()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			Attendee[] toReturn = new Attendee[] {new Attendee("a", "b"), new Attendee("c", "d")};
			SetupResult.For(_service.GetAttendees(_conference, 0, 2)).Return(toReturn);
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service);
			controller.ListAttendees("austincodecamp2008", 0, 2);

			Assert.That(controller.ActualViewName, Is.EqualTo("listattendees"));
			Conference viewDataConference = controller.ViewData["conference"] as Conference;
			IEnumerable<AttendeeListing> viewDataAttendee = controller.ViewData["attendees"] as IEnumerable<AttendeeListing>;
			Assert.That(viewDataConference, Is.Not.Null);
			Assert.That(viewDataAttendee, Is.Not.Null);
			Assert.That(viewDataConference, Is.EqualTo(_conference));

			List<AttendeeListing> listingList = new List<AttendeeListing>(viewDataAttendee);
			Assert.That(listingList.Count, Is.EqualTo(2));
			Assert.That(listingList[0].Name, Is.EqualTo("a b"));
			Assert.That(listingList[1].Name, Is.EqualTo("c d"));
		}
    }
}