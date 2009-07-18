using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface ITimeSlotRepository : IRepository<TimeSlot>
    {
        TimeSlot[] GetAllForConference(Conference conference);
    }
}