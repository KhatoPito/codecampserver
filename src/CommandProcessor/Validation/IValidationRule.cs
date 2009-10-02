namespace Tarantino.RulesEngine
{
	public interface IValidationRule
	{
		bool StopProcessing { get; }
		string IsValid(object input);
	}

	public interface ICrossReferencedValidationRule : IValidationRule
	{
		object ToCompare { get; set; }
	}
}