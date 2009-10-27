using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core;
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
			DateTime startDate = DateTime.Today.Midnight().AddHours(8).AddDays(-7*5);
			for (int i = 0; i < 6; i++)
			{
				DateTime meetingDate = startDate.AddHours(1.5*i);

				yield return new Meeting
				             	{
				             		Address = "500 E Cesar Chavez St",
				             		City = "Austin",
				             		Description = "Monospace Tutorial",
				             		EndDate = meetingDate.AddHours(1.25),
				             		StartDate = meetingDate,
				             		Key = meetingDate.Month.ToString().ToLower() + meetingDate.Day + "meeting",
				             		LocationName = "Austin Convention Center",
				             		Name = "Session " +i +" Tutorial",
				             		PostalCode = "78701",
				             		Region = "Texas",
									Topic = "iPhone Development with MonoTouch",
				             		Summary =
				             			"MonoTouch allows developers to create C# and .NET based applications that run on Apple's iPhone and Apple's iPod Touch devices, while taking advantage of the iPhone APIs and reusing both code and libraries that have been built for .NET, as well as existing skills. ",
				             		LocationUrl = "http://maps.google.com",
				             		TimeZone = "CST",
									SpeakerName = "Geoff Norton",
				             		SpeakerBio =
				             			"He is a smart dude.",
				             		SpeakerUrl = "http://monospace.us"
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