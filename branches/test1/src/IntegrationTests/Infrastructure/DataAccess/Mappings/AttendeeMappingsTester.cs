using July09v31.Core.Domain.Model;
using NUnit.Framework;

namespace July09v31.IntegrationTests.Infrastructure.DataAccess.Mappings
{
    [TestFixture]
    public class AttendeeMappingsTester : DataTestBase
    {
        [Test]
        public void Should_map_an_Attendee()
        {
            var attendee = new Attendee
                               {
                                   EmailAddress = "jdoe@abc.com",
                                   FirstName = "sdf",
                                   Webpage = "http://thisdoemain.com",
                                   LastName = "lastname ",
                                   Status = AttendanceStatus.Interested
                               };

            AssertObjectCanBePersisted(attendee);
        }
    }
}