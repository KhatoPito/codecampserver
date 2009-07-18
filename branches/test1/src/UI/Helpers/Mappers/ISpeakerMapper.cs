using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Helpers.Mappers
{
    public interface ISpeakerMapper : IMapper<Speaker, SpeakerForm>
    {
        SpeakerForm[] Map(Speaker[] speakers);
    }
}