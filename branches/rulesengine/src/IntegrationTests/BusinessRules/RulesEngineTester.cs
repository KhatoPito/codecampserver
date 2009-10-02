using System;
using System.Collections.Generic;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Views;
using CommandProcessor;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.Mvc;

namespace CodeCampServer.UnitTests.Core.Services
{

	[TestFixture]
	public class RulesEngineTester : TestBase
	{		
		[Test]
		public void The_rules_engine_should_process_a_ui_message()
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
	}
}