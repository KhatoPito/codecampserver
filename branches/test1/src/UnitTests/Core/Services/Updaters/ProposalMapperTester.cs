using System;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Messages;
using July09v31.Core.Domain.Model;
using July09v31.Core.Domain.Model.Enumerations;
using July09v31.Core.Domain.Model.Planning;
using July09v31.Core.Services.Bases;
using July09v31.UI.Helpers.Mappers;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace July09v31.UnitTests.Core.Services.Updaters
{
    [TestFixture]
    public class ProposalMapperTester : TestBase
    {
        [Test]
        public void Should_map_a_new_proposal()
        {
            var conference = new Conference();
            var track = new Track();

            var message = S<IProposalMessage>();
            message.Id = Guid.Empty;
            message.ConferenceKey = "conf";
            message.Title = "title";
            message.Level = SessionLevel.L200;
            message.Track = track;
            message.Abstract = "abstract";

            var conferenceRepository = S<IConferenceRepository>();
            conferenceRepository.Stub(x => x.GetByKey("conf")).Return(conference);
            var clock = S<ISystemClock>();
            clock.Stub(x => x.GetCurrentDateTime()).Return(new DateTime(2000, 1, 1));

            var mapper = new ProposalMapper(S<IProposalRepository>(), conferenceRepository, clock);
            Proposal proposal = mapper.Map(message);

            proposal.IsPersistent.ShouldBeFalse();
            proposal.Conference.ShouldEqual(conference);
            proposal.Title.ShouldEqual("title");
            proposal.Level.ShouldEqual(SessionLevel.L200);
            proposal.Track.ShouldEqual(track);
            proposal.Abstract.ShouldEqual("abstract");
            proposal.Status.ShouldEqual(ProposalStatus.Draft);
        }

        [Test]
        public void Should_map_an_existing_proposal()
        {
            Guid proposalId = Guid.NewGuid();
            var existingProposal = new Proposal { Id = proposalId };
            var conference = new Conference();
            var track = new Track();

            var message = S<IProposalMessage>();
            message.Id = proposalId;
            message.ConferenceKey = "conf";
            message.Title = "title";
            message.Level = SessionLevel.L200;
            message.Track = track;
            message.Abstract = "abstract";

            var conferenceRepository = S<IConferenceRepository>();
            conferenceRepository.Stub(x => x.GetByKey("conf")).Return(conference);
            var proposalRepository = S<IProposalRepository>();
            proposalRepository.Stub(x => x.GetById(proposalId)).Return(existingProposal);
            var clock = S<ISystemClock>();
            clock.Stub(x => x.GetCurrentDateTime()).Return(new DateTime(2000, 1, 1));

            var mapper = new ProposalMapper(proposalRepository, conferenceRepository, clock);

            Proposal proposal = mapper.Map(message);

            proposal.IsPersistent.ShouldBeTrue();
            proposal.Conference.ShouldEqual(conference);
            proposal.Title.ShouldEqual("title");
            proposal.Level.ShouldEqual(SessionLevel.L200);
            proposal.Track.ShouldEqual(track);
            proposal.Abstract.ShouldEqual("abstract");
            proposal.Status.ShouldEqual(ProposalStatus.Draft);
        }
    }
}