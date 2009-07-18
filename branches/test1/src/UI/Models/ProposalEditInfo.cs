using July09v31.Core.Domain.Model.Planning;

namespace July09v31.UI.Models
{
    public class ProposalEditInfo
    {
        public IStateCommand[] Commands { get; set; }
        public bool ReadOnly { get; set; }
    }
}