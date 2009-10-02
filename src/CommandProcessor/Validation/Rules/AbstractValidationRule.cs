namespace Tarantino.RulesEngine.ValidationRules
{
	public abstract class AbstractCrossReferenceValidationRule<TTarget> : ICrossReferencedValidationRule
	{
		#region ICrossReferencedValidationRule Members

		public string IsValid(object toCheck)
		{
			return IsValidCore((TTarget) toCheck, (TTarget) ToCompare);
		}

		public object ToCompare { get; set; }

		public virtual bool StopProcessing
		{
			get { return false; }
		}

		#endregion

		protected abstract string IsValidCore(TTarget toCheck, TTarget toCompare);

		protected string Success()
		{
			return null;
		}
	}
}