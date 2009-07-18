using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface ITrackRepository : IRepository<Track>
    {
        Track[] GetAllForConference(Conference conference);
    }
}