using System;
using System.Collections.Generic;

using Microsoft.Practices.ServiceLocation;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;
using Tarantino.RulesEngine.Configuration;
using Tarantino.RulesEngine.Mvc;

namespace CommandProcessor.UnitTests
{
	[TestFixture]
	public class RulesEngineTester
	{
		[Test]
		public void the_engine_should_create_a_message_processor()
		{
			ServiceLocator.SetLocatorProvider(() => new FakeLocator());
			var rulesEngine = new RulesEngine();
			RulesEngine.MessageProcessorFactory = new FakeFactory();
			
			rulesEngine.Initialize(typeof (TestMessage).Assembly,new FakeMessageMapper());
			ExecutionResult result = rulesEngine.Process(new TestMessage(), typeof (TestMessage));
			result.Successful.ShouldBeTrue();
		}

		[Test]
		public void the_engine_should_store_configuration_and_mapping_as_a_static()
		{
			var rulesEngine = new RulesEngine();
			rulesEngine.Initialize(typeof (TestMessage).Assembly,new FakeMessageMapper());
			rulesEngine.Configuration.ShouldNotBeNull();
		}
	}

	public class FakeMessageMapper : IMessageMapper
	{
		public ICommandMessage MapUiMessageToCommandMessage(IMessage message, Type messageType, Type destinationType)
		{
			return new TestCommand();
		}
	}

	public class FakeFactory : IMessageProcessorFactory
	{
		public MessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration, WebContext webContext)
		{
			return new MessageProcessor(mappingEngine,
										new CommandInvoker(new ValidationEngine(new ValidationRuleFactory()),new CommandFactory()), unitOfWork, 
										configuration,new OriginalFormRetriever(new FakeWebContext(), new Serializer(new Base64Utility())));
		}

		public MessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration, IWebContext webContext)
		{
			throw new NotImplementedException();
		}
	}

	public class FakeWebContext : IWebContext
	{
		#region IWebContext Members

		public string GetRequestItem(string key)
		{
			return null;
		}

		#endregion
	}

	public class FakeLocator : IServiceLocator
	{
		#region IServiceLocator Members

		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public object GetInstance(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public object GetInstance(Type serviceType, string key)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return new List<object>();
		}

		public TService GetInstance<TService>()
		{
			return (TService) ((object)new FakeUoW());
		}

		public TService GetInstance<TService>(string key)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TService> GetAllInstances<TService>()
		{
			throw new NotImplementedException();
		}

		#endregion
	}

	public class FakeUoW:IUnitOfWork
	{
		public void Dispose()
		{
			
		}

		public void Begin()
		{
			
		}

		public void RollBack()
		{
		
		}

		public void Commit()
		{
		
		}
	}

	public class TestMessage : IMessage //Defined in UI project?
	{
	}

	//Defined in UI project?
	public class TestMessageConfiguration : MessageDefinition<TestMessage>
	{
		public TestMessageConfiguration()
		{
			Execute<TestCommand>();
		}
	}


	//Domain Message
	public class TestCommand : ICommandMessage//ICommandMessage defined in Core
	{
	}

	//Domain Service
	public class TestCommandHandler: Command<TestCommand>
	{
		protected override ReturnValue Execute(TestCommand commandMessage)
		{
			throw new NotImplementedException();
		}
	}
}