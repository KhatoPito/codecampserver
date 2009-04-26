using System;
using CodeCampServer.Core.Domain.Messages;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.Core.Domain.Model.Planning;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class ProposalForm : IProposalMessage
	{
		public string ConferenceKey { get; set; }

		[BetterValidateNonEmpty("Track")]
		public Track Track { get; set; }

		[BetterValidateNonEmpty("Level")]
		public SessionLevel Level { get; set; }

		[BetterValidateNonEmpty("Title")]
		public string Title { get; set; }

		[BetterValidateNonEmpty("Abstract")]
		public string Abstract { get; set; }

		[Hidden]
		public Guid Id { get; set; }

        public int Votes
        {
            get; set ;
        }

	    [NoInput]
		public ProposalStatus Status { get; set; }

		[NoInput]
		public string SubmissionDate { get; set; }

		public bool IsTransient()
		{
			return Id == Guid.Empty;
		}
	}
}