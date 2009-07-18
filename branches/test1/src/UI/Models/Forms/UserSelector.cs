using System;
using System.ComponentModel;
using Castle.Components.Validator;
using July09v31.UI.Helpers.Attributes;
using July09v31.UI.Helpers.Binders;
using July09v31.UI.Helpers.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    [TypeConverter(typeof(UserFormTypeConverter))]
    public class UserSelector
    {
        [BetterValidateNonEmpty("Name")]
        public virtual string Name { get; set; }

        [Hidden]
        public Guid Id { get; set; }

        [BetterValidateNonEmpty("Username")]
        [ValidateRegExp(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
            "Username is not valid.")]
        public virtual string Username { get; set; }

    }
}