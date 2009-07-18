using July09v31.Core.Domain.Model;

namespace July09v31.Core.Services
{
    public interface IUserSession
    {
        User GetCurrentUser();
        void LogIn(User user);
        void LogOut();
    }
}