using System;
using July09v31.Core;
using July09v31.UI.Helpers.Attributes;
using July09v31.UI.Helpers.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    public class TrackForm : ValueObject<TrackForm>
    {
        public virtual Guid Id { get; set; }

        [BetterValidateNonEmpty("Name")]
        public virtual string Name { get; set; }

        public virtual Guid ConferenceId { get; set; }

        [Hidden]
        public virtual string ConferenceKey { get; set; }
    }
}