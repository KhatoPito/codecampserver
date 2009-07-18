using System;
using July09v31.Core;
using July09v31.Core.Domain.Model;
using July09v31.UI.Helpers.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    public class SponsorForm : ValueObject<SponsorForm>
    {
        public virtual Guid? ID { get; set; }

        [BetterValidateNonEmpty("Name")]
        public virtual string Name { get; set; }

        [BetterValidateNonEmpty("Url")]
        public virtual string Url { get; set; }

        [BetterValidateNonEmpty("Banner Url")]
        public virtual string BannerUrl { get; set; }

        public virtual SponsorLevel Level { get; set; }

        public Guid ParentID { get; set; }
    }
}