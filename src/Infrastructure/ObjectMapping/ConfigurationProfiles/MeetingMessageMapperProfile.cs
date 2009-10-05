using AutoMapper;
using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Messages;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
{
	public class MeetingMessageMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<DeleteMeetingMessage, DeleteMeetingCommandMessage>();
		}
	}
}