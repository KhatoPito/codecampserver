using System.Collections.Generic;
using System.Linq;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using NHibernate;

namespace July09v31.Infrastructure.DataAccess.Impl
{
    public class TrackRepository : RepositoryBase<Track>, ITrackRepository
    {
        public TrackRepository(ISessionBuilder sessionFactory)
            : base(sessionFactory)
        {
        }

        public Track[] GetAllForConference(Conference conference)
        {
            ISession session = GetSession();
            IQuery query = session.CreateQuery("from Track t where t.Conference = ? order by t.Name");
            query.SetParameter(0, conference);
            IList<Track> list = query.List<Track>();
            return list.ToArray();
        }
    }
}