using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class MeetingRepository : KeyedRepository<Meeting>, IMeetingRepository
	{
		public MeetingRepository(ISessionBuilder sessionFactory,IUnitOfWork unitOfWork) : base(sessionFactory) {}
	}
}