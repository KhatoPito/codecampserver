using System;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class MeetingMapper : IMeetingMapper
	{
		private readonly IMappingEngine _mappingEngine;
		private readonly IMeetingRepository _meetingRepository;

		public MeetingMapper(IMappingEngine mappingEngine,IMeetingRepository meetingRepository)
		{
			_mappingEngine = mappingEngine;
			_meetingRepository = meetingRepository;
		}

		public MeetingInput Map(Meeting model)
		{
			return _mappingEngine.Map<Meeting, MeetingInput>(model);
		}

		public TMessage2 Map<TMessage2>(Meeting model)
		{
			return _mappingEngine.Map<Meeting, TMessage2>(model);
		}

		public Meeting Map(MeetingInput message)
		{
			return _mappingEngine.Map<MeetingInput, Meeting>(message);
		}

		public MeetingAnnouncementDisplay[] Map(Meeting[] meetings)
		{
			return meetings.Select(meeting => _mappingEngine.Map<Meeting,MeetingAnnouncementDisplay>(meeting)).ToArray();
		}
	}
}