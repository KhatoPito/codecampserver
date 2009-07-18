using System;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Messages;
using July09v31.Core.Domain.Model.Planning;
using July09v31.Core.Services.Bases;

namespace July09v31.UI.Helpers.Mappers
{
    public class ProposalMapper : AutoFormMapper<Proposal, IProposalMessage>, IProposalMapper
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ISystemClock _clock;

        public ProposalMapper(IProposalRepository proposalRepository, IConferenceRepository conferenceRepository, ISystemClock clock)
            : base(proposalRepository)
        {
            _conferenceRepository = conferenceRepository;
            _clock = clock;
        }

        protected override Guid GetIdFromMessage(IProposalMessage message)
        {
            return message.Id;
        }

        protected override void MapToModel(IProposalMessage message, Proposal model)
        {
            model.Title = message.Title;
            model.Level = message.Level;
            model.Track = message.Track;
            model.Abstract = message.Abstract;
            model.Votes = message.Votes;
            model.Conference = _conferenceRepository.GetByKey(message.ConferenceKey);
        }
    }
}