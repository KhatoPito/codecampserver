using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeCampServer.UI.Models.Input
{
	public class UserGroupInput
	{
		//public UserGroupInput()
		//{
		//    Users = new List<UserSelectorInput>();
		//    Sponsors = new List<SponsorInput>();
		//}

		[Required]
		//[ValidateKey]
			public virtual string Key { get; set; }

		public virtual Guid Id { get; set; }

		[Required]
		public virtual string Name { get; set; }

		public virtual string DomainName { get; set; }
		public virtual string HomepageHTML { get; set; }

		public virtual string City { get; set; }

		public virtual string Region { get; set; }

		public virtual string Country { get; set; }

		public virtual string GoogleAnalysticsCode { get; set; }

		public virtual IList<UserSelectorInput> Users { get; set; }

		public virtual IList<SponsorInput> Sponsors { get; set; }

		public string Location()
		{
			string location = "";
			if (!string.IsNullOrEmpty(City))
			{
				location = City + ", " + Region + " - ";
			}
			location += Country;
			return location;
		}
	}
}