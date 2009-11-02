using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	public class MeetingAnnouncementDisplay : IKeyable, IGloballyUnique
	{
		public Guid Id { get; set; }
		public string Key { get; set; }
		public string Heading { get; set; }
		public string Topic { get; set; }
		public string Summary { get; set; }
		public DateTimeSpan When { get; set; }
		public string LocationName { get; set; }
		public string LocationUrl { get; set; }

		[Label("Speaker")]
		public string SpeakerName { get; set; }

		public string SpeakerUrl { get; set; }
		public string SpeakerBio { get; set; }
		public string MeetingInfo { get; set; }
	}
}