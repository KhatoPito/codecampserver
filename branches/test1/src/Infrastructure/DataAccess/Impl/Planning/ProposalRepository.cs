using System.Linq;
using July09v31.Core.Domain.Model;
using July09v31.Core.Domain.Model.Planning;

namespace July09v31.Infrastructure.DataAccess.Impl.Planning
{
    public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
    {
        public ProposalRepository(ISessionBuilder sessionFactory) : base(sessionFactory) { }

        public Proposal[] GetByConference(Conference conference)
        {
            return GetSession()
                .CreateQuery("from Proposal p where p.Conference = ? order by p.CreatedDate desc")
                .SetEntity(0, conference).List<Proposal>().ToArray();
        }
    }
}