using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.RulesEngine;

namespace Tarantino.UnitTests.Core.Services.OperationHandling.Engine
{
	public static class ExecutionResultAssertionExtensions
	{
		public static void ShouldHaveMessage<TMessage>(this ExecutionResult result,
		                                               Expression<Func<TMessage, object>> expression)
		{
			string key = UINameHelper.BuildNameFrom(expression);
			result.ShouldHaveMessage(key);
		}

		public static void ShouldNotBeSuccessful(this ExecutionResult result)
		{
			Assert.That(result.Successful, Is.False);
		}

		public static void ShouldHaveMessage(this ExecutionResult result, string key)
		{
			if (result.Messages.Where(x => x.IncorrectAttribute == key).Count() == 0)
			{
				string failureMessage = result.Messages.Select(x => x.IncorrectAttribute + ":" + x.MessageText).WrapEachWith("", "",
				                                                                                                             "\n");
				Assert.Fail(string.Format("No message for {0}.  Other messages include:\n{1}", key, failureMessage));
				return;
			}
		}

		public static void ShouldBeSuccessful(this ExecutionResult result)
		{
			if (!result.Successful)
			{
				string failureMessage = result.Messages.Select(x => x.IncorrectAttribute + ":" + x.MessageText).WrapEachWith("", "",
				                                                                                                             "\n");
				Assert.Fail(string.Format("Not successful. Messages include:\n{0}", failureMessage));
			}
		}
	}
}