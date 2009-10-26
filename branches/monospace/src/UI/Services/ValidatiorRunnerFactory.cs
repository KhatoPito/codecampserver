using Castle.Components.Validator;

namespace CodeCampServer.Infrastructure.UI.Services
{
	public class ValidatiorRunnerFactory
	{
		public static ValidatiorRunnerFactory Default { get; set; }
		public IValidatorRunner Create()
		{
			return new ValidatorRunner(new CachedValidationRegistry());
		}
	}
}