using CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.BusinessRules
{
	public class UpdateUserGroupMessageConfiguration : MessageDefinition<UserGroupInput>
	{

		public UpdateUserGroupMessageConfiguration()
		{
			Execute<UpdateUserGroupCommandMessage>();
		}		
	}
}