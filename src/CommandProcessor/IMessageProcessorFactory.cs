using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;
using Tarantino.RulesEngine.Configuration;
using Tarantino.RulesEngine.Mvc;

namespace CommandProcessor.UnitTests
{
	public interface IMessageProcessorFactory
	{
		MessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration,
		                        IWebContext webContext);
	}
}