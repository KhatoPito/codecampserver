namespace July09v31.Core.Domain.Model.Planning
{
    public interface IProposalRepository : IRepository<Proposal>
    {
        Proposal[] GetByConference(Conference conference);
    }
}