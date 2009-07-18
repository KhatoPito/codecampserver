using System;
using July09v31.Core.Domain.Model;
using July09v31.Core.Domain.Model.Enumerations;
using July09v31.UI.Helpers.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    public class SessionForm : EditForm<SessionForm>
    {
        public override Guid Id { get; set; }

        [BetterValidateNonEmpty("Url Key")]
        public virtual string Key { get; set; }
        public virtual Track Track { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }
        public virtual Speaker Speaker { get; set; }
        public virtual Conference Conference { get; set; }
        public virtual string RoomNumber { get; set; }
        [BetterValidateNonEmpty("Title")]
        public virtual string Title { get; set; }
        public virtual string Abstract { get; set; }
        public virtual SessionLevel Level { get; set; }
        public virtual string MaterialsUrl { get; set; }
    }
}