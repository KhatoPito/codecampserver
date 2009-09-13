using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class EventRepositoryTester : KeyedRepositoryTester<Event, EventRepository>
	{
		private static Conference CreateConference()
		{
			var conference = new Conference
			                 	{
			                 		Name = "sdf",
			                 		Description = "description",
			                 		StartDate = new DateTime(2008, 12, 2),
			                 		EndDate = new DateTime(2008, 12, 3),
			                 		LocationName =
			                 			"St Edwards Professional Education Center",
			                 		Address = "12343 Research Blvd",
			                 		City = "Austin",
			                 		Region = "Tx",
			                 		PostalCode = "78234",
			                 		PhoneNumber = "512-555-1234"
			                 	};
			conference.AddAttendee(new Attendee {EmailAddress = "werwer@asdfasd.com"});
			return conference;
		}

		private static Meeting CreateMeeting()
		{
			var meeting = new Meeting
			              	{
			              		Name = "sdf",
			              		Description = "description",
			              		StartDate = new DateTime(2008, 12, 2),
			              		EndDate = new DateTime(2008, 12, 3),
			              		LocationName = "St Edwards Professional Education Center",
			              		Address = "12343 Research Blvd",
			              		City = "Austin",
			              		Region = "Tx",
			              		PostalCode = "78234",
			              		LocationUrl = "http://foobar",
			              		Topic = "topic",
			              		Summary = "summary",
			              		SpeakerName = "speakername",
			              		SpeakerBio = "bio",
			              		SpeakerUrl = "http://google.com"
			              	};
			return meeting;
		}

		protected override EventRepository CreateRepository()
		{
			return new EventRepository(GetSessionBuilder());
		}

		[Test]
		public void should_retrieve_events_for_a_usergroup()
		{
			var usergroup = new UserGroup();
			var conference1 = new Conference {UserGroup = usergroup};
			var meeting1 = new Meeting {UserGroup = usergroup};
			var meeting2 = new Meeting();

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(meeting1);
				session.SaveOrUpdate(meeting2);
				session.Flush();
			}

			IEventRepository repository = new EventRepository(new HybridSessionBuilder());
			Event[] events = repository.GetAllForUserGroup(usergroup);

			events.Length.ShouldEqual(2);
		}

		[Test]
		public void Should_retrieve_upcoming_conferences_for_a_usergroup()
		{
			SystemTime.Now = () => new DateTime(2009, 5, 5);
			var usergroup = new UserGroup();
			var conference1 = new Conference {UserGroup = usergroup, EndDate = new DateTime(2009, 4, 6)};
			var conference4 = new Conference {UserGroup = usergroup, EndDate = new DateTime(2009, 5, 4, 20, 0, 0)};
			var conference2 = new Conference {UserGroup = usergroup, EndDate = new DateTime(2009, 5, 5, 20, 0, 0)};
			var conference3 = new Conference {UserGroup = usergroup, EndDate = new DateTime(2009, 5, 7)};

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(conference2);
				session.SaveOrUpdate(conference3);
				session.SaveOrUpdate(conference4);
				session.Flush();
			}

			IConferenceRepository repository = new ConferenceRepository(new HybridSessionBuilder());
			Conference[] conferences = repository.GetFutureForUserGroup(usergroup);

			conferences.Length.ShouldEqual(2);
			conferences[0].ShouldEqual(conference2);
		}

		[Test]
		public void Should_update_an_attendee()
		{
			Conference conference = CreateConference();
			conference.GetAttendees()[0].Status = AttendanceStatus.Interested;

			IConferenceRepository repository = new ConferenceRepository(new HybridSessionBuilder());

			repository.Save(conference);
			conference.GetAttendees()[0].Status = AttendanceStatus.Confirmed;
			repository.Save(conference);

			Conference rehydratedConference = repository.GetById(conference.Id);
			rehydratedConference.GetAttendees()[0].Status.ShouldEqual(
				AttendanceStatus.Confirmed);
		}
	}
}