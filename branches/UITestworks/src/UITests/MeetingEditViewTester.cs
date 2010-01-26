using System;
using System.Collections.Generic;
using System.Configuration;
using CodeCampServer.UI;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using UITestHelper;
using UITestHelper.WatiN;
using WatiN.Core;

namespace CodeCampServerUiTests
{
    [TestFixture]
    public class MeetingEditViewTester
    {
        private IBrowserDriver _driver;

        [TestFixtureSetUp]
        public void Setup()
        {
            string baseurl = ConfigurationManager.AppSettings["url"];

            _driver = new WatinDriver(new IE(), baseurl);

            InputWrapperFactory.Factory = () => new InputWrapperFactoryOverride();
        }

        [TestFixtureTearDown]
        public void teardown()
        {
            _driver.Dispose();
            _driver = null;
        }


        public InputForm<T> Form<T>(string url)
        {
            return new InputForm<T>(_driver.Navigate(url));
        }

        [Test]
        public void Should_create_a_new_meeting()
        {
            _driver.ScreenCaptureOnFailure(() =>
               {
                   Form<LoginInputProxy>("/login/login/index")
                       .Input(m => m.Username, "admin")
                       .Input(m => m.Password, "password")
                       .Submit();

                   Form<MeetingInput>("/Meeting/New")
                       .Input(m => m.Name, "TX")
                       .Input(m => m.Topic, "my topic")
                       .Input(m => m.Summary, "this will be a normal meeting")
                       .Input(m => m.Description, "The description")
                       .Input(m => m.Key, Guid.NewGuid().ToString())
                       .Input(m => m.LocationName, "our location")
                       .Input(m => m.LocationUrl, "http://foolocation.com")
                       .Input(m => m.SpeakerBio, "this is a great speaker")
                       .Input(m => m.SpeakerName, "bart simpson")
                       .Input(m => m.SpeakerUrl, "http://thesimpsons.com")
                       .Input(m => m.TimeZone, "CST")
                       .Input(m => m.StartDate, "12/11/2010 12:00 pm")
                       .Input(m => m.EndDate, "12/11/2010 1:00 pm")
                       .Submit();

                   _driver.Url.ShouldBe("/home");
               });
        }
    }

    public static class AssertExtensions
    {
        public static string ShouldBe(this string actualValue, string expectedValue)
        {
            Assert.That(actualValue, Is.EqualTo(expectedValue));
            return actualValue;
        }
    }
}