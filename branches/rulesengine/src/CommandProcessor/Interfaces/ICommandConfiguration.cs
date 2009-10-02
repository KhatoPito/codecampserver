using System;
using System.Collections.Generic;

namespace Tarantino.RulesEngine.CommandProcessor
{
	public interface ICommandConfiguration
	{
		Type CommandMessageType { get; }
		Delegate Condition { get; }
		void Initialize(ICommandMessage commandMessage, ExecutionResult result);
		IEnumerable<ValidationRuleInstance> GetValidationRules();
	}
}