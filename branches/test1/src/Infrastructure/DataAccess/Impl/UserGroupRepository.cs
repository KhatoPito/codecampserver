using System;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;

namespace July09v31.Infrastructure.DataAccess.Impl
{
    public class UserGroupRepository : KeyedRepository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(ISessionBuilder sessionFactory) : base(sessionFactory) { }
        public UserGroup GetDefaultUserGroup()
        {
            return GetByKey("localhost");
        }
    }
}