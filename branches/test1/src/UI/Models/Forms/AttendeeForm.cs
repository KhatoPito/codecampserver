using System;
using July09v31.Core;
using July09v31.Core.Domain.Model;
using July09v31.UI.Helpers.Validation.Attributes;
using July09v31.UI.Models.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    public class AttendeeForm : ValueObject<AttendeeForm>
    {
        public virtual Guid ConferenceID { get; set; }
        public virtual Guid? AttendeeID { get; set; }

        [BetterValidateNonEmpty("First Name")]
        public virtual string FirstName { get; set; }

        [BetterValidateNonEmpty("Last Name")]
        public virtual string LastName { get; set; }

        [BetterValidateNonEmpty("Email")]
        [BetterValidateEmail("Email")]
        public virtual string EmailAddress { get; set; }
        public virtual AttendanceStatus Status { get; set; }

        public virtual string Webpage { get; set; }
    }
}