using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class UserGroupRepository : KeyedRepository<UserGroup>, IUserGroupRepository
	{
		public UserGroupRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public UserGroup GetDefaultUserGroup()
		{
			return GetByKey("localhost");
		}
	}
}