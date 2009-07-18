using July09v31.Core.Domain.Model;

namespace July09v31.Core.Services
{
    public interface IAuthenticationService
    {
        bool PasswordMatches(User user, string password);
    }
}