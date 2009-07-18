using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface IConferenceRepository : IKeyedRepository<Conference>
    {
        Conference GetNextConference();
        Conference[] GetAllForUserGroup(UserGroup usergroup);
        Conference[] GetFutureForUserGroup(UserGroup usergroup);
    }
}