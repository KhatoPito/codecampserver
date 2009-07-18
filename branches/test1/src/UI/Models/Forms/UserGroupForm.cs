using System;
using System.Collections.Generic;
using July09v31.Core.Domain.Model;
using July09v31.UI.Helpers.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    public class UserGroupForm
    {
        [BetterValidateNonEmpty("User Group Key")]
        [ValidateKey]
        public virtual string Key { get; set; }

        public virtual Guid Id { get; set; }

        [BetterValidateNonEmpty("Name")]
        public virtual string Name { get; set; }

        public virtual string DomainName { get; set; }
        public virtual string HomepageHTML { get; set; }

        public virtual string City { get; set; }

        public virtual string Region { get; set; }

        public virtual string Country { get; set; }

        public virtual string GoogleAnalysticsCode { get; set; }

        public virtual IList<UserSelector> Users { get; set; }

        public virtual IList<SponsorForm> Sponsors { get; set; }
    }
}