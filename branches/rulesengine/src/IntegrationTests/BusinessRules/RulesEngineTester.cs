using System;
using System.Collections.Generic;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.BusinessRules;
using CodeCampServer.Infrastructure.ObjectMapping;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;
using CodeCampServer.UnitTests;
using CommandProcessor;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.Mvc;

namespace CodeCampServer.IntegrationTests.BusinessRules
{
	[TestFixture]
	public class RulesEngineTester : TestBase
	{		
		[Test]
		public void DeleteMeeting_message_should_delete_a_meeting()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof(IUnitOfWork), S<IUnitOfWork>());
			ObjectFactory.Inject(typeof(IWebContext), S<IWebContext>());

			var repository = S<IRepository<Meeting>>();
			var meeting = new Meeting();
			repository.Stub(repository1 => repository1.GetById(Guid.Empty)).IgnoreArguments().Return(meeting);
			ObjectFactory.Inject(typeof(IRepository<Meeting>), repository);
			ObjectFactory.Inject(typeof(IMeetingRepository), S<IMeetingRepository>());

			RulesEngineConfiguration.Configure(typeof(DeleteMeetingMessageConfiguration));
			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new DeleteMeetingMessage(){Meeting = Guid.NewGuid()}, typeof (DeleteMeetingMessage));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<Meeting>().ShouldEqual(meeting);
		}

		[Test]
		public void UpdateMeeting_should_save_a_meeting()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof(IUnitOfWork), S<IUnitOfWork>());
			ObjectFactory.Inject(typeof(IWebContext), S<IWebContext>());

			var repository = S<IRepository<Meeting>>();
			ObjectFactory.Inject(typeof(IRepository<Meeting>), repository);
			var meetingRepository = S<IMeetingRepository>();
			ObjectFactory.Inject(typeof(IMeetingRepository), meetingRepository);

			var userGroupRepository = S<IUserGroupRepository>();
			userGroupRepository.Stub(groupRepository => groupRepository.GetById(Guid.Empty)).Return(new UserGroup());
			ObjectFactory.Inject(typeof(IUserGroupRepository), userGroupRepository);


			RulesEngineConfiguration.Configure(typeof(UpdateMeetingMessageConfiguration));
			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new MeetingInput() { Description = "New Meeting" }, typeof(MeetingInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<Meeting>().ShouldNotBeNull();

			meetingRepository.AssertWasCalled(r=>r.Save(null),options => options.IgnoreArguments());
		}

		[Test]
		public void UpdateUserGroup_should_save_a_usergroup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof(IUnitOfWork), S<IUnitOfWork>());
			ObjectFactory.Inject(typeof(IWebContext), S<IWebContext>());

			var repository = S<IRepository<UserGroup>>();
			ObjectFactory.Inject(typeof(IRepository<UserGroup>), repository);
			
			var userGroupRepository = S<IUserGroupRepository>();
			ObjectFactory.Inject(typeof(IUserGroupRepository), userGroupRepository);

			var userRepository = S<IUserRepository>();
			ObjectFactory.Inject(typeof(IUserRepository), userRepository);

			RulesEngineConfiguration.Configure(typeof(UpdateUserGroupMessageConfiguration));
			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new UserGroupInput()
			                                 	{
			                                 		Name = "New Meeting",
													Users = new List<UserSelectorInput>(),
													Sponsors = new List<SponsorInput>(),
			                                 	}, typeof(UserGroupInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<UserGroup>().ShouldNotBeNull();

			userGroupRepository.AssertWasCalled(r => r.Save(null), options => options.IgnoreArguments());
		}
	}
}