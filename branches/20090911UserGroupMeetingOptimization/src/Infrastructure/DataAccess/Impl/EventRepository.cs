using System.Linq;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class EventRepository : KeyedRepository<Event>, IEventRepository
	{
		public EventRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}

		public Event[] GetAllForUserGroup(UserGroup usergroup)
		{
			return GetSession().CreateQuery(
				"from Conference conf where conf.UserGroup = :usergroup order by conf.StartDate desc").
				SetEntity("usergroup",
				          usergroup).List<Conference>().ToArray();
		}

		public Event[] GetFutureForUserGroup(UserGroup usergroup)
		{
			return GetSession().CreateQuery(
				"from Conference conf where conf.UserGroup = :usergroup and conf.EndDate >= :datetime order by conf.StartDate")
				.SetEntity("usergroup", usergroup)
				.SetDateTime("datetime", SystemTime.Now().Midnight())
				.List<Conference>().ToArray();
		}
	}
}