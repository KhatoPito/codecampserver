using System;
using July09v31.Core.Domain.Messages;
using July09v31.Core.Domain.Model;
using July09v31.Core.Domain.Model.Enumerations;
using July09v31.Core.Domain.Model.Planning;
using July09v31.UI.Helpers.Attributes;
using July09v31.UI.Helpers.Validation.Attributes;

namespace July09v31.UI.Models.Forms
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
            get;
            set;
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