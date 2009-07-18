using Castle.Components.Validator;

namespace July09v31.UI.Models.Forms
{
    public class LoginForm
    {
        [ValidateNonEmpty]
        public string Username { get; set; }

        [ValidateNonEmpty]
        public string Password { get; set; }
    }
}