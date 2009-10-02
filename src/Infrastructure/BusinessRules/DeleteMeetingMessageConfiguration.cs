using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Messages;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.UnitTests.Core.Services
{
	public class DeleteMeetingMessageConfiguration : MessageDefinition<DeleteMeetingMessage>
	{

		public DeleteMeetingMessageConfiguration()
		{
			Execute<DeleteMeetingCommandMessage>();
		}		
	}
}