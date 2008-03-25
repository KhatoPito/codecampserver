using System;
using System.Collections.Generic;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SponsorControllerTester
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
            _authorizationService = _mocks.CreateMock<IAuthorizationService>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
        }

        #endregion

        private MockRepository _mocks;
        private IConferenceRepository _conferenceRepository;
        private Conference _conference;
        private IAuthorizationService _authorizationService;

        private class TestingSponsorController : SponsorController
        {
            public string ActualMasterName;
            public object ActualViewData;
            public string ActualViewName;
            public RouteValueDictionary RedirectToActionValues;

            public TestingSponsorController(IConferenceRepository conferenceRepository,
                                            IAuthorizationService authorizationService, IClock clock)
                : base(conferenceRepository, authorizationService, clock)
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
                RedirectToActionValues = values;
            }
        }

        private TestingSponsorController GetController()
        {
            var controller =
                new TestingSponsorController(_conferenceRepository, _authorizationService, new ClockStub());
            return controller;
        }

        [Test]
        public void DeleteShouldRemoveSponsorAndRenderList()
        {
            var sponsorToDelete = new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum);
            var sponsor = new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum);
            _conference.AddSponsor(sponsor);
            _conference.AddSponsor(sponsorToDelete);
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            Expect.Call(delegate { _conferenceRepository.Save(_conference); });
            _mocks.ReplayAll();

            TestingSponsorController controller = GetController();

            controller.Delete(_conference.Key, "name");
            Assert.That(controller.ViewData.Contains<Sponsor[]>(), Is.True);
            var sponsors =
                new List<Sponsor>(controller.ViewData.Get<Sponsor[]>());
            Assert.That(sponsors.Contains(sponsorToDelete), Is.False);
            Assert.That(controller.ActualViewName, Is.EqualTo("List"));
        }

        [Test]
        public void EditSponsorShouldGetSponsorData()
        {
            _conference.AddSponsor(new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold));
            SetupResult.For(_authorizationService.IsAdministrator).Return(true);
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);

            _mocks.ReplayAll();

            TestingSponsorController controller = GetController();
            controller.Edit("austincodecamp2008", "test");

            var viewDataSponsor = controller.ViewData.Get<Sponsor>();
            Assert.That(viewDataSponsor.Name, Is.EqualTo("test"));
            Assert.That(controller.ActualViewName, Is.EqualTo("edit"));
        }

        [Test]
        public void EditSponsorShouldRedirectToListWhenNoSponsor()
        {
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            SetupResult.For(_authorizationService.IsAdministrator).Return(true);
            _mocks.ReplayAll();
            TestingSponsorController controller = GetController();
            controller.Edit("austincodecamp2008", null);
            Assert.That(controller.RedirectToActionValues, Is.Not.Null);
            Assert.That(controller.RedirectToActionValues["Controller"], Is.EqualTo("Sponsor"));
            Assert.That(controller.RedirectToActionValues["Action"], Is.EqualTo("List"));
        }

        //[Test]
        //public void EditSponsorShouldRedirectToLoginWhenNotAuthorizedToEdit()
        //{
        //    SetupResult.For(_conferenceRepository.GetConference("austincodecamp2008")).Return(_conference);
        //    SetupResult.For(_authorizationService.IsAdministrator).Return(false);
        //    _mocks.ReplayAll();
        //    TestingSponsorController controller = GetController();
        //    controller.Edit("austincodecamp2008", null);
        //    Assert.That(controller.RedirectToActionValues, Is.Not.Null);
        //    Assert.That(controller.RedirectToActionValues["Controller"], Is.EqualTo("login"));
        //}

        [Test]
        public void NewActionShouldRenderEditViewWithNewSponsor()
        {
            TestingSponsorController controller = GetController();
            controller.New(_conference.Key);

            Assert.That(controller.ViewData.Contains<Sponsor>());
            Assert.That(controller.ActualViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void SaveShouldProperlyEditExistingSponsor()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            Expect.Call(delegate { _conferenceRepository.Save(_conference); }).Repeat.Twice();
            _mocks.ReplayAll();

            TestingSponsorController controller = GetController();
            controller.Save(_conference.Key, "name", "edited name", "Gold", "", "", "", "", "");

            Assert.That(controller.ActualViewName, Is.EqualTo("list"));
            Assert.That(controller.ViewData, Is.Not.Null);

            Assert.That(controller.ViewData.Get<Sponsor[]>().Length, Is.EqualTo(2));
            Assert.That(
                Array.Exists(controller.ViewData.Get<Sponsor[]>(),
                             delegate(Sponsor s) { return s.Name == "edited name"; }),
                Is.True);
            Assert.That(
                Array.Exists(controller.ViewData.Get<Sponsor[]>(), delegate(Sponsor s) { return s.Name == "name"; }),
                Is.False);
        }

        [Test]
        public void SaveShouldSaveSponsor()
        {
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            Expect.Call(delegate { _conferenceRepository.Save(_conference); });
            _mocks.ReplayAll();

            TestingSponsorController controller = GetController();
            controller.Save(_conference.Key, "", "name", "Bronze", "logoUrl", "website", "firstName", "lastName",
                            "email");
            Assert.That(controller.ActualViewName, Is.EqualTo("list"));
            Assert.That(controller.ViewData, Is.Not.Null);

            Assert.That(Array.Exists(controller.ViewData.Get<Sponsor[]>(), delegate(Sponsor s) { return s.Name == "name"; }),
                        Is.True);
        }

        [Test]
        public void ShouldListSponsorsForAConference()
        {
            _conference.AddSponsor(new Sponsor("name", "logourl", "website", "", "", "", SponsorLevel.Platinum));
            _conference.AddSponsor(new Sponsor("name2", "logourl2", "website2", "", "", "", SponsorLevel.Bronze));

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);

            _mocks.ReplayAll();

            var controller =
                new TestingSponsorController(_conferenceRepository, _authorizationService, new ClockStub());
            controller.List("austincodecamp2008");
            Assert.That(controller.ActualViewName, Is.EqualTo("List"));

            Assert.That(controller.ViewData, Is.Not.Null);
            Assert.That(controller.ViewData.Contains<Sponsor[]>());

            Sponsor[] sponsors = controller.ViewData.Get<Sponsor[]>();
            Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Bronze));
            Assert.That(sponsors[0].Name, Is.EqualTo("name"));
            Assert.That(sponsors[1].Name, Is.EqualTo("name2"));
        }
    }
}