using System;

namespace Tarantino.RulesEngine.CommandProcessor
{
	public interface IMessageMapper
	{
		ICommandMessage MapUiMessageToCommandMessage(IMessage message, Type messageType, Type destinationType);
	}
}