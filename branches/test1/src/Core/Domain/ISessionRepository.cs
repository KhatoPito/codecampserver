using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface ISessionRepository : IKeyedRepository<Session>
    {
        Session[] GetAllForConference(Conference conference);
        Session[] GetAllForTimeSlot(TimeSlot timeSlot);
        Session[] GetAllForTrack(Track track);
        Session[] GetAllForSpeaker(Speaker speaker);
    }
}