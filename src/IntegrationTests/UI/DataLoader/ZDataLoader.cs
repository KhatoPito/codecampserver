using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.Impl;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.UI.DataLoader
{
	[TestFixture]
	public class ZDataLoader : DataTestBase
	{
		[Test, Category("DataLoader")]
		public void DataLoader()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			DeleteAllObjects();
			LoadData();
		}

		private void LoadData()
		{
			var list = new List<PersistentObject>();
			list.AddRange(CreateMeetings().ToArray());
			list.AddRange(CreateUsers().ToArray());
			PersistEntities(list.ToArray());
		}

		private IEnumerable<Meeting> CreateMeetings()
		{
			DateTime startDate = DateTime.Now.AddDays(-7*5);
			for (int i = 0; i < 6; i++)
			{
				DateTime meetingDate = startDate.AddDays(7*i);
				yield return new Meeting
				             	{
				             		Address = "123 Guadalupe Street",
				             		City = "Austin",
				             		Description = "Regular meeting.  Don't forget CodeCamp planning next month!",
				             		EndDate = meetingDate.AddDays(1),
				             		StartDate = meetingDate,
				             		Key = meetingDate.Month.ToString().ToLower() + meetingDate.Day + "meeting",
				             		LocationName = "St. Edward's Professional Education Center",
				             		Name = meetingDate.ToString("MMMM") + " meeting",
				             		PostalCode = "78787",
				             		Region = "Texas",
				             		//UserGroup = userGroup,
				             		Topic = "ASP.NET MVC in Action",
				             		Summary =
				             			"With the new version of ASP.NET, developers can easily leverage the Model-View-Controller pattern in ASP.NET applications. Pulling logic away from the UI and the views has been difficult for a long time. The Model-View-Presenter pattern helps a little bit, but the fact that the view has to delegate to the presenter makes the UI pattern difficult to work with. This session is a detailed overview of the ASP.NET MVC Framework.  It is meant for developers already building systems with ASP.NET 3.5 SP1.",
				             		LocationUrl = "http://maps.google.com",
				             		TimeZone = "CST",
				             		SpeakerName = "Jeffrey Palermo",
				             		SpeakerBio =
				             			"Jeffrey Palermo is the CTO of Headspring Systems. Jeffrey specializes in Agile management coaching and helps companies double the productivity of software teams. He is instrumental in the Austin software community as a member of AgileAustin and a director of the Austin .Net User Group. Jeffrey has been recognized by Microsoft as a “Microsoft Most Valuable Professional” (MVP) for technical and community leadership. He is also certified as a MCSD.Net and ScrumMaster. Jeffrey has spoken and facilitated at industry conferences such as VSLive, DevTeach, and Microsoft Tech Ed. He also speaks to user groups around the country as part of the INETA Speakers’ Bureau. His web sites are headspringsystems.com and jeffreypalermo.com. He is a graduate from Texas A&M University, an Eagle Scout, and an Iraq war veteran.  Jeffrey is the founder of the CodeCampServer open-source project and a co-founder of the MvcContrib project.",
				             		SpeakerUrl = "http://jeffreypalermo.com"
				             	};
			}
		}

		private User[] CreateUsers()
		{
			var mapper = new UserMapper(new UserRepository(GetSessionBuilder()), new Cryptographer());
			return new[]
			       	{
			       		mapper.Map(new UserInput
			       		           	{
			       		           		Name = "Joe User",
			       		           		Username = "admin",
			       		           		EmailAddress = "joe@user.com",
			       		           		Password = "password"
			       		           	}),
			       		mapper.Map(new UserInput
			       		           	{
			       		           		Name = "Eric Hexter",
			       		           		EmailAddress = "jeffis@theman.com",
			       		           		Username = "eric",
			       		           		Password = "pepsi"
			       		           	}),
			       		mapper.Map(new UserInput
			       		           	{
			       		           		Name = "Homer Simpson",
			       		           		EmailAddress = "homer@simpsons.com",
			       		           		Username = "hsimpson",
			       		           		Password = "drpepper"
			       		           	}),
			       		mapper.Map(new UserInput
			       		           	{
			       		           		Name = "Bart Simpson",
			       		           		EmailAddress = "bart@simpsons.com",
			       		           		Username = "bsimpson",
			       		           		Password = "coke"
			       		           	})
			       	};
		}

		private static int _seed;

		private static int GetRandomInt()
		{
			return new Random(_seed++).Next(100);
		}
	}
}