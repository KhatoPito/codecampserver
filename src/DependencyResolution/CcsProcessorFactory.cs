using System;
using CommandProcessor.UnitTests;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.DependencyResolution
{
	public class CcsProcessorFactory : IMessageProcessorFactory
	{
		public MessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine,
		                               CommandEngineConfiguration configuration)
		{
			throw new NotImplementedException();
		}
	}
}