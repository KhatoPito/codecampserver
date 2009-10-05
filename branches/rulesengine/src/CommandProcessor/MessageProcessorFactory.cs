using CommandProcessor.UnitTests;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;
using Tarantino.RulesEngine.Configuration;
using Tarantino.RulesEngine.Mvc;

namespace CommandProcessor
{
	public class MessageProcessorFactory : IMessageProcessorFactory
	{
		public MessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine,
		                               CommandEngineConfiguration configuration, IWebContext webContext)
		{
			return new MessageProcessor(mappingEngine,
			                            new CommandInvoker(new ValidationEngine(new ValidationRuleFactory()),
			                                               new CommandFactory()), unitOfWork, configuration,
			                            new OriginalFormRetriever(webContext, new Serializer(new Base64Utility())));
		}
	}
}