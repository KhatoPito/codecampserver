using System;
using July09v31.Core.Domain.Model;
using July09v31.Core.Domain.Model.Planning;
using July09v31.Infrastructure.DataAccess.Impl.Planning;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace July09v31.IntegrationTests.Infrastructure.DataAccess
{
    [TestFixture]
    public class ProposalRepositoryTester : RepositoryTester<Proposal, ProposalRepository>
    {
        [Test]
        public void Should_retrive_all_proposals_for_a_conference()
        {
            var conference1 = new Conference();
            var conference2 = new Conference();
            var proposal1 = new Proposal { Conference = conference1, CreatedDate = new DateTime(2000, 1, 1) };
            var proposal2 = new Proposal { Conference = conference1, CreatedDate = new DateTime(1999, 1, 1) };
            var proposal3 = new Proposal { Conference = conference2, CreatedDate = new DateTime(2000, 1, 1) };

            PersistEntities(conference1, conference2, proposal1, proposal2, proposal3);

            IProposalRepository repository = CreateRepository();
            Proposal[] proposals = repository.GetByConference(conference1);
            proposals.Length.ShouldEqual(2);
            proposals[0].ShouldEqual(proposal1);
            proposals[1].ShouldEqual(proposal2);
        }

        protected override ProposalRepository CreateRepository()
        {
            return new ProposalRepository(GetSessionBuilder());
        }
    }
}