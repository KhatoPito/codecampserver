using System;
using System.Collections;
using System.Collections.Generic;
using Tarantino.RulesEngine.Configuration;


namespace Tarantino.RulesEngine.CommandProcessor
{
	public interface IValidationEngine
	{
		ExecutionResult ValidateCommand(ICommandMessage commandMessage, ICommandConfiguration commandConfiguration);
	}

	public class ValidationEngine : IValidationEngine
	{
		private readonly IValidationRuleFactory _ruleFactory;

		public ValidationEngine(IValidationRuleFactory ruleFactory)
		{
			_ruleFactory = ruleFactory;
		}

		#region IValidationEngine Members

		public ExecutionResult ValidateCommand(ICommandMessage commandMessage, ICommandConfiguration commandConfiguration)
		{
			var totalResult = new ExecutionResult();

			IEnumerable<ValidationRuleInstance> ruleInstances = commandConfiguration.GetValidationRules();

			foreach (ValidationRuleInstance instance in ruleInstances)
			{
				if (instance.ShouldApply != null)
					if (!(bool) instance.ShouldApply.DynamicInvoke(commandMessage))
						continue;

				Delegate compile = instance.ToCheckExpression.Compile();
				object input = compile.DynamicInvoke(new object[] {commandMessage});
				bool stopProcessing = false;

				if (instance.ArrayRule)
				{
					var enumerable = (IEnumerable) input;

					int i = 0;

					foreach (object item in enumerable)
					{
						if (item == null) continue;

						IValidationRule rule = _ruleFactory.ConstructRule(instance);
						string result = rule.IsValid(item);

						bool ruleFailed = result != null;

						if (ruleFailed)
						{
							string uiName = UINameHelper.BuildNameFrom(instance.UIAttributeExpression);
							string label = UINameHelper.BuildLabelFrom(instance.UIAttributeExpression);
							if (instance.ToCompareExpression != null)
							{
								string otherName = UINameHelper.BuildLabelFrom(instance.ToCheckExpression);
								totalResult.AddMessage(uiName.Replace(MessageDefinition<IMessage>.INDEX.ToString(), i.ToString()),
								                       string.Format(result, label, otherName));
							}
							else
							{
								totalResult.AddMessage(uiName.Replace(MessageDefinition<IMessage>.INDEX.ToString(), i.ToString()),
								                       string.Format(result, label));
							}

							if (rule.StopProcessing)
							{
								stopProcessing = true;
								break;
							}
						}
						i++;
					}
				}
				else
				{
					IValidationRule rule = _ruleFactory.ConstructRule(instance);

					if (rule is ICrossReferencedValidationRule)
					{
						Delegate toCompareDelegate = instance.ToCompareExpression.Compile();
						object toCompare = toCompareDelegate.DynamicInvoke(new object[] {commandMessage});
						((ICrossReferencedValidationRule) rule).ToCompare = toCompare;
					}

					string result = rule.IsValid(input);

					bool ruleFailed = result != null;

					if (ruleFailed)
					{
						string uiName = UINameHelper.BuildNameFrom(instance.UIAttributeExpression);
						string label = UINameHelper.BuildLabelFrom(instance.UIAttributeExpression);
						if (instance.ToCompareExpression != null)
						{
							string otherName = UINameHelper.BuildLabelFrom(instance.ToCheckExpression);
							totalResult.AddMessage(uiName, string.Format(result, label, otherName));
						}
						else
						{
							totalResult.AddMessage(uiName, string.Format(result, label));
						}

						if (rule.StopProcessing)
						{
							break;
						}
					}
				}
				if (stopProcessing)
					break;
			}

			return totalResult;
		}

		#endregion
	}
}