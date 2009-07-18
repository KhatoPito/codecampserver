using System;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Helpers.Mappers
{
    public class TrackMapper : AutoFormMapper<Track, TrackForm>, ITrackMapper
    {
        private readonly IConferenceRepository _conferenceRepository;

        public TrackMapper(ITrackRepository repository, IConferenceRepository conferenceRepository)
            : base(repository)
        {
            _conferenceRepository = conferenceRepository;
        }

        protected override Guid GetIdFromMessage(TrackForm form)
        {
            return form.Id;
        }

        protected override void MapToModel(TrackForm form, Track model)
        {
            model.Name = form.Name;
            model.Conference = _conferenceRepository.GetById(form.ConferenceId);
        }

        public TrackForm[] Map(Track[] tracks)
        {
            return Map<Track[], TrackForm[]>(tracks);
        }
    }
}