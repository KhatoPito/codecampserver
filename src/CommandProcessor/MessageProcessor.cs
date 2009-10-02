using System;
using System.Collections.Generic;
using Tarantino.RulesEngine.Configuration;

namespace Tarantino.RulesEngine.CommandProcessor
{
	public interface IMessageProcessor
	{
		ExecutionResult Process(IMessage message, Type messageType);
	}

	public class MessageProcessor : IMessageProcessor
	{
		private readonly ICommandInvoker _commandInvoker;
		private readonly CommandEngineConfiguration _configuration;
		private readonly IMessageMapper _mappingEngine;
		private readonly IOriginalFormRetriever _originalFormRetriever;
		private readonly IUnitOfWork _unitOfWork;

		public MessageProcessor(IMessageMapper mappingEngine, ICommandInvoker commandInvoker, IUnitOfWork unitOfWork,
		                        CommandEngineConfiguration configuration, IOriginalFormRetriever originalFormRetriever
			)
		{
			_mappingEngine = mappingEngine;
			_commandInvoker = commandInvoker;
			_unitOfWork = unitOfWork;
			_configuration = configuration;
			_originalFormRetriever = originalFormRetriever;
		}

		#region IMessageProcessor Members

		public ExecutionResult Process(IMessage message, Type messageType) 
		{
			var totalResult = new ExecutionResult();
			IMessageConfiguration messageConfiguration = _configuration.GetMessageConfiguration(messageType);

			IMessage original = (IMessage) _originalFormRetriever.Retrieve(messageType);

			IEnumerable<ICommandConfiguration> commandConfigurations = messageConfiguration.GetApplicableCommands(original,message);

			foreach (ICommandConfiguration commandConfiguration in commandConfigurations)
			{
				ICommandMessage commandMessage =
					_mappingEngine.MapUiMessageToCommandMessage(message, messageType, commandConfiguration.CommandMessageType);

				commandConfiguration.Initialize(commandMessage, totalResult);

				try
				{
					ExecutionResult results = _commandInvoker.Process(commandMessage, commandConfiguration);
					totalResult.MergeWith(results);
				}
				catch
				{
					_unitOfWork.RollBack();
					throw;
				}

				if (!totalResult.Successful)
				{
					_unitOfWork.RollBack();
					break;
				}
			}

			if (totalResult.Successful)
			{
				_unitOfWork.Commit();
			}

			return totalResult;
		}


		#endregion

		
	}
}