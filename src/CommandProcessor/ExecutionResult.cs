using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tarantino.RulesEngine
{
	public class ExecutionResult
	{
		private readonly List<ErrorMessage> _messages = new List<ErrorMessage>();
		private readonly GenericItemDictionary _returnItems = new GenericItemDictionary();

		public ExecutionResult()
		{
		}

		public ExecutionResult(GenericItemDictionary returnItems)
		{
			_returnItems = returnItems;
		}

		public bool Successful
		{
			get { return _messages.Count == 0; }
		}

		public List<ErrorMessage> Messages
		{
			get { return new List<ErrorMessage>(_messages); }
		}

		public GenericItemDictionary ReturnItems
		{
			get { return _returnItems; }
		}

		public ExecutionResult AddMessage<TOperation>(Expression<Func<TOperation, object>> attributeExpression, string message)
		{
			string attribute = UINameHelper.BuildNameFrom(attributeExpression);
			_messages.Add(new ErrorMessage {IncorrectAttribute = attribute, MessageText = message});
			return this;
		}

		public ExecutionResult AddMessage<TMessage, TSecondMessage>(
			Expression<Func<TMessage, TSecondMessage>> messageExpression,
			Expression<Func<TSecondMessage, object>> secondMessageExpression,
			string message)
		{
			string key1 = UINameHelper.BuildNameFrom(messageExpression);
			string key2 = UINameHelper.BuildNameFrom(secondMessageExpression);

			string key = key1 + "." + key2;

			_messages.Add(new ErrorMessage {IncorrectAttribute = key, MessageText = message});
			return this;
		}

		public ExecutionResult AddMessage(string key, string message)
		{
			_messages.Add(new ErrorMessage {IncorrectAttribute = key, MessageText = message});
			return this;
		}

		public void MergeWith(params ExecutionResult[] otherResults)
		{
			if (otherResults == null || otherResults.Length == 0) return;

			foreach (ExecutionResult otherResult in otherResults)
			{
				foreach (ErrorMessage message in otherResult.Messages)
				{
					_messages.Add(new ErrorMessage {IncorrectAttribute = message.IncorrectAttribute, MessageText = message.MessageText});
				}

				foreach (KeyValuePair<Type, object> returnItem in otherResult.ReturnItems)
				{
					_returnItems.Add(returnItem.Key, returnItem.Value);
				}
			}
		}

		public void Merge(ReturnValue returnObject)
		{
			if (returnObject != null && returnObject.Type != null)
			{
				ReturnItems.Add(returnObject.Type, returnObject.Value);
			}
		}
	}
}