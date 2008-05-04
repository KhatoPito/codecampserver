using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using NHibernate;

namespace CodeCampServer.DataAccess.Impl
{
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        public SessionRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        public void Save(Session session)
        {
            ISession dataSession = getSession();
            ITransaction transaction = dataSession.BeginTransaction();
            dataSession.SaveOrUpdate(session);
            transaction.Commit();
        }

        public IEnumerable<Session> GetProposedSessions(Conference conference)
        {
            IQuery query = getSession().CreateQuery(
                @"from Session s join fetch s.Conference 
                 where s.Conference = ? and s.IsApproved = false");
            query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));
            return query.List<Session>();
        }
    }
}
