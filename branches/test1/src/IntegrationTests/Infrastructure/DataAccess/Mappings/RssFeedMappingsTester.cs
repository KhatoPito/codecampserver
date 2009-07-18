using System;
using July09v31.Core.Domain.Model;
using NUnit.Framework;

namespace July09v31.IntegrationTests.Infrastructure.DataAccess.Mappings
{
    [TestFixture]
    public class RssFeedMappingsTester : DataTestBase
    {
        [Test]
        public void Should_map_feed()
        {
            var feed = new RssFeed() { Name = "name", Url = "url" };

            AssertObjectCanBePersisted(feed);
        }
    }
}