using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface ISpeakerRepository : IKeyedRepository<Speaker>
    {
        Speaker[] GetAllForConference(Conference conference);
    }
}