using Castle.Components.Validator;

namespace July09v31.UI.Helpers.Validation.Attributes
{
    public class ValidateKeyAttribute : ValidateRegExpAttribute
    {
        //[ValidateRegExp(@"^[A-Za-z0-9\-_]+$", "Key should only contain letters, numbers, and hypens.")]
        public ValidateKeyAttribute()
            : base(@"^[A-Za-z0-9\-_]+$", "Key should only contain letters, numbers, and hypens.")
        {

        }
    }
}