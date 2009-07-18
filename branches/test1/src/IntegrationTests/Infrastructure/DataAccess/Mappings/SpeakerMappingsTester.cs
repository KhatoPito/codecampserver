using July09v31.Core.Domain.Model;
using NUnit.Framework;

namespace July09v31.IntegrationTests.Infrastructure.DataAccess.Mappings
{
    public class SpeakerMappingsTester : DataTestBase
    {
        [Test]
        public void Should_map_Speaker()
        {
            var conference = new Conference();
            var speaker = new Speaker
                            {
                                Bio = "bio",
                                Company = "company",
                                EmailAddress = "email",
                                FirstName = "first",
                                LastName = "last",
                                JobTitle = "title",
                                Key = "key",
                                WebsiteUrl = "url",
                                Conference = conference
                            };
            AssertObjectCanBePersisted(conference);
            AssertObjectCanBePersisted(speaker);

        }
    }
}