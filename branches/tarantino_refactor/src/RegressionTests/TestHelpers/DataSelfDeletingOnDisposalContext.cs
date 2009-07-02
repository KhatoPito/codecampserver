using System;
using CodeCampServer.Core.Domain.Model;

namespace RegressionTests
{
    public class DataSelfDeletingOnDisposalContext :IDisposable
    {
        private readonly DataCreationHelper creationHelper;
        private readonly PersistentObject[] entities;

        public DataSelfDeletingOnDisposalContext(DataCreationHelper creationHelper, PersistentObject[] entities)
        {
            this.creationHelper = creationHelper;
            this.entities = entities;
        }

        public void Dispose()
        {
            creationHelper.DeleteEntities(entities);
        }
    }
}