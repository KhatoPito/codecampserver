using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Helpers.Mappers
{
    public interface IUserMapper : IMapper<User, UserForm>
    {
        UserForm[] Map(User[] model);
        User[] Map(UserForm[] message);
    }
}