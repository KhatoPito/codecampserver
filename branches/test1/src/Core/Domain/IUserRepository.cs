using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface IUserRepository : IKeyedRepository<User>
    {
        User GetByUserName(string username);
        User[] GetLikeLastNameStart(string query);
    }
}