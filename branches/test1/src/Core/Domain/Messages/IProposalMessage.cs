using System;
using July09v31.Core.Domain.Model;
using July09v31.Core.Domain.Model.Enumerations;

namespace July09v31.Core.Domain.Messages
{
    public interface IProposalMessage
    {
        string ConferenceKey { get; set; }
        Track Track { get; set; }
        SessionLevel Level { get; set; }
        string Title { get; set; }
        string Abstract { get; set; }
        Guid Id { get; set; }
        int Votes { get; set; }
    }
}