using System;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.InputBuilders;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;
using StructureMap;
using UITestHelper;
using WatiN.Core;

namespace CodeCampServerUiTests
{
	[TestFixture]
	public class MeetingEditViewTester
	{
		private IE _ie;

		[SetUp]
		public void Setup()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			var baseurl = System.Configuration.ConfigurationManager.AppSettings["url"];
			_ie = new IE(baseurl + "/Meeting/New");
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			throw new NotImplementedException();
		}

		[TearDown]
		public void teardown()
		{
			_ie.Dispose();
		}

		public FluentForm<T> Form<T>()
		{
			return new FluentForm<T>(new WatinDriver(_ie), _ie);
		}

		[Test]
		public void Should_create_a_new_meeting()
		{
			Form<MeetingInput>()
				.WithTextInput(m => m.Name, "TX")
				.WithTextInput(m => m.Topic, "my topic")
				.WithTextInput(m => m.Summary, "this will be a normal meeting")
				.WithTextBoxInput(m => m.Description, "The description")
				.WithTextInput(m => m.Key, "foe")
				.WithTextInput(m => m.LocationName, "our location")
				.WithTextInput(m => m.LocationUrl, "http://foolocation.com")
				.WithTextBoxInput(m => m.SpeakerBio, "this is a great speaker")
				.WithTextInput(m => m.SpeakerName, "bart simpson")
				.WithTextInput(m => m.SpeakerUrl, "http://thesimpsons.com")
				.WithTextInput(m => m.TimeZone, "CST")
				.WithDateInput(m => m.EndDate, new DateTime(2009, 12, 12))
				.WithDateInput(m => m.StartDate, new DateTime(2009, 12, 11))
				.Submit();
		}
	}
}