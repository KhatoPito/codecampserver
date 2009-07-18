using July09v31.Core.Domain.Model.Planning;

namespace July09v31.Core.Services
{
    public interface IProposalCoordinator
    {
        IStateCommand[] GetValidCommands(Proposal proposal);
        void ExecuteCommand(Proposal proposal, string commandName);
    }
}