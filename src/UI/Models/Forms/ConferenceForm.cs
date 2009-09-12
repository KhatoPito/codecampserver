using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class ConferenceForm : EventForm
	{
		[BetterValidateNonEmpty("Time Zone")]
		public virtual string TimeZone { get; set; }

		[Label("Phone Number")]
		public virtual string PhoneNumber { get; set; }

		public virtual bool HasRegistration { get; set; }

		public string HtmlContent { get; set; }
	}
}