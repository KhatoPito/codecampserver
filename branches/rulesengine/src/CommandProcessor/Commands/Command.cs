namespace Tarantino.RulesEngine.CommandProcessor
{
	public abstract class Command<T> : ICommandMessageHandler where T : ICommandMessage
	{
		#region ICommandMessageHandler Members

		public ReturnValue Execute(ICommandMessage commandMessage)
		{
			return Execute((T) commandMessage);
		}

		#endregion

		protected abstract ReturnValue Execute(T commandMessage);
	}

	public interface ICommandMessageHandler : ICommandMessageHandler<ICommandMessage, ReturnValue> {}

	public interface ICommandMessageHandler<TCommand, TResult>
	{
		TResult Execute(TCommand commandMessage);
	}
}