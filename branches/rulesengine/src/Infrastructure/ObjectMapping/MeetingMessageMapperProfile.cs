using AutoMapper;
using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Messages;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class MeetingMessageMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<DeleteMeetingMessage, DeleteMeetingCommandMessage>();
		}
	}
}