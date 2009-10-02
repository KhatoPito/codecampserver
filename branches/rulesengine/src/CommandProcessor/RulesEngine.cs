using System;
using System.Reflection;
using CommandProcessor.UnitTests;
using Microsoft.Practices.ServiceLocation;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;
using Tarantino.RulesEngine.Configuration;
using Tarantino.RulesEngine.Mvc;

namespace CommandProcessor
{
	public interface IRulesEngine {
		ExecutionResult Process(IMessage message, Type messageType);
		ExecutionResult Process(IMessage message);
	}

	public class RulesEngine : IRulesEngine
	{
		private static CommandEngineConfiguration _configuration;
		public static IMessageProcessorFactory MessageProcessorFactory = new MessageProcessorFactory();
		public static object _lock = new object();

		private static IMessageMapper _mappingEngine;

		public CommandEngineConfiguration Configuration
		{
			get { return _configuration; }
			private set { _configuration = value; }
		}

		public IMessageMapper MappingEngine
		{
			get { return _mappingEngine; }
			private set { _mappingEngine = value; }
		}

		public void Initialize(Assembly assembly,IMessageMapper messageMapper)
		{
			if (_mappingEngine == null)
			{
				lock (_lock)
				{
					if (_configuration == null)
					{
						Configuration = new CommandEngineConfiguration();
						Configuration.Initialize(assembly);
						_mappingEngine = messageMapper;
					}
				}
			}
		}
		private IServiceLocator locator
		{
			get
			{
				try
				{
					return ServiceLocator.Current;
				}
				catch (Exception)
				{

					throw new InvalidOperationException("The Microsoft.Practices.ServiceLocation.ServiceLocator must be initialized by calling the SetLocatorProvider method.");
				}
			}
		}
		public ExecutionResult Process(IMessage message, Type messageType)
		{			
			var unitOfWork = locator.GetInstance<IUnitOfWork>();
			var context = locator.GetInstance<IWebContext>();
			var processor = MessageProcessorFactory.Create(unitOfWork, MappingEngine, Configuration, context);
			return processor.Process(message,messageType);
		}

		public ExecutionResult Process(IMessage message)
		{
			return Process(message, message.GetType());
		}
	}
}