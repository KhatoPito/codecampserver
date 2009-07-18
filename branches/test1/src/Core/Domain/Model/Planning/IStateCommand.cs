namespace July09v31.Core.Domain.Model.Planning
{
    public interface IStateCommand
    {
        bool IsValid(Proposal proposal, User currentUser);
        void Execute(Proposal proposal, User currentUser);
        string TransitionVerbPresentTense { get; }
        bool Matches(string commandName);
    }
}