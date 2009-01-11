using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IUserMapper : IModelUpdater<User, UserForm>, IMapper<UserForm, User>, IMapper<User, UserForm>
	{
		
	}
}