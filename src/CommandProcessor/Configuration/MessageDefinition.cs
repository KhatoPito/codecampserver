using System.Collections.Generic;
using System.Linq;
using Tarantino.RulesEngine.CommandProcessor;

namespace Tarantino.RulesEngine.Configuration
{
	public interface IMessageConfiguration
	{
		IEnumerable<ICommandConfiguration> GetApplicableCommands(IMessage original, IMessage revised);
	}

	public abstract class MessageDefinition<TMessage> : IMessageConfiguration
		where TMessage : IMessage
	{
		public const int INDEX = 999999;
		private static readonly ConditionExpression _conditions = new ConditionExpression();

		private readonly List<CommandDefinition> _CommandDefinitions = new List<CommandDefinition>();

		protected IConditionExpression<TMessage> Conditions
		{
			get { return _conditions; }
		}

		#region IMessageConfiguration Members

		IEnumerable<ICommandConfiguration> IMessageConfiguration.GetApplicableCommands(IMessage original, IMessage revised)
		{
			return _CommandDefinitions
				.Where(op => (bool) op.Condition.DynamicInvoke(new[] {original, revised}))
				.Cast<ICommandConfiguration>();
		}

		#endregion

		public ICommandConfigurationExpression<TMessage, TCommand> Execute<TCommand>()
			where TCommand : ICommandMessage
		{
			var CommandConfiguration = new CommandDefinition(typeof (TCommand));

			_CommandDefinitions.Add(CommandConfiguration);

			return new CommandConfigurationExpression<TMessage, TCommand>(CommandConfiguration);
		}

		#region Nested type: ConditionExpression

		private class ConditionExpression : IConditionExpression<TMessage>
		{
		}

		#endregion
	}

	public interface IConditionExpression<TMessage>
	{
	}
}