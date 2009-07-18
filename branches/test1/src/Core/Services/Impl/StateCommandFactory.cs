using System;
using System.Collections.Generic;
using July09v31.Core.Domain.Model.Planning;
using July09v31.Core.Domain.Model.Planning.StateCommands;

namespace July09v31.Core.Services.Impl
{
    public class StateCommandFactory : IStateCommandFactory
    {
        private List<IStateCommand> _stateCommands = new List<IStateCommand>();

        public StateCommandFactory(SaveDraftCommand saveDraftCommand, DraftToSubmittedCommand draftToSubmittedCommand,
            SubmittedToAcceptedCommand submittedToAcceptedCommand, AcceptedToConfirmedCommand acceptedToConfirmedCommand)
        {
            _stateCommands.Add(saveDraftCommand);
            _stateCommands.Add(draftToSubmittedCommand);
            _stateCommands.Add(submittedToAcceptedCommand);
            _stateCommands.Add(acceptedToConfirmedCommand);
        }
        public IStateCommand[] GetAllStateCommands()
        {
            return _stateCommands.ToArray();
        }
    }
}