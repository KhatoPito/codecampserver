using CodeCampServer.Core.Services.BusinessRule.CreateMeeting;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.BusinessRules
{
	public class CreateMeetingMessageConfiguration : MessageDefinition<MeetingInput>
	{

		public CreateMeetingMessageConfiguration()
		{
			Execute<CreateMeetingCommandMessage>();
		}		
	}
}