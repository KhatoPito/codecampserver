using July09v31.Core.Domain.Model;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Helpers.Mappers
{
    public interface IScheduleMapper
    {
        ScheduleForm[] Map(Conference conference);
    }
}