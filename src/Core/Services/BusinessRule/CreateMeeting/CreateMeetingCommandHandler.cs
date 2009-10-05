using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.CreateMeeting
{
	public class CreateMeetingCommandHandler : Command<CreateMeetingCommandMessage>
	{
		private readonly IMeetingRepository _meetingRepository;

		public CreateMeetingCommandHandler(IMeetingRepository meetingRepository)
		{
			_meetingRepository = meetingRepository;
		}

		protected override ReturnValue Execute(CreateMeetingCommandMessage commandMessage)
		{
			_meetingRepository.Save(commandMessage.Meeting);
			return new ReturnValue {Type = typeof (Meeting), Value = commandMessage.Meeting};
		}
	}
}