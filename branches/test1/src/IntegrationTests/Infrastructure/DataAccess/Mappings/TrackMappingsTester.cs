using System;
using July09v31.Core.Domain.Model;
using NUnit.Framework;

namespace July09v31.IntegrationTests.Infrastructure.DataAccess.Mappings
{
    public class TrackMappingsTester : DataTestBase
    {
        [Test]
        public void Should_map_track()
        {
            var conference = new Conference { StartDate = new DateTime(2001, 1, 1), EndDate = new DateTime(2001, 1, 2) };
            var track = new Track { Conference = conference, Name = "Test" };
            PersistEntity(conference);
            AssertObjectCanBePersisted(track);
        }
    }
}