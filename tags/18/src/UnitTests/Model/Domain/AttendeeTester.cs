using CodeCampServer.Model.Domain;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class AttendeeTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Attendee();
        }
    }
}