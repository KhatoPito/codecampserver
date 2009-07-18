using Castle.Components.Validator;
using July09v31.UI.Models.Validation.Validators;

namespace July09v31.UI.Models.Validation.Attributes
{
    public class ValidateDateNotInFutureAttribute : AbstractValidationAttribute
    {
        public ValidateDateNotInFutureAttribute()
        {
        }

        public ValidateDateNotInFutureAttribute(string errorMessage)
            : base(errorMessage)
        {
        }

        public override IValidator Build()
        {
            IValidator validator = new DateNotInFutureValidator();

            ConfigureValidatorMessage(validator);

            return validator;
        }
    }
}