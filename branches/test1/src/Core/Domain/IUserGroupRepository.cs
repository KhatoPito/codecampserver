using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface IUserGroupRepository : IKeyedRepository<UserGroup>
    {
        UserGroup GetDefaultUserGroup();
    }
}