using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface ITimeSlotMapper : IMapper<TimeSlot, TimeSlotForm>
	{
		TimeSlotForm[] Map(TimeSlot[] timeSlots);
	}
}