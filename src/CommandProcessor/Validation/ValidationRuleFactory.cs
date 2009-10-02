using Microsoft.Practices.ServiceLocation;

namespace Tarantino.RulesEngine.CommandProcessor
{
	public interface IValidationRuleFactory
	{
		IValidationRule ConstructRule(ValidationRuleInstance ruleInstance);
	}

	public class ValidationRuleFactory : IValidationRuleFactory
	{
		#region IValidationRuleFactory Members

		public IValidationRule ConstructRule(ValidationRuleInstance ruleInstance)
		{
			return (IValidationRule)
			       ServiceLocator.Current.GetInstance(ruleInstance.ValidationRuleType);
		}
		#endregion
	}
}