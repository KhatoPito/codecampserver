using System;
using July09v31.Core;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace July09v31.UnitTests.Core.Services
{
    [TestFixture]
    public class SystemTimeTester
    {
        [Test]
        public void Returns_current_date_time()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime systemTime = SystemTime.Now();
            TimeSpan value = systemTime - currentDateTime;

            value.Seconds.ShouldBeLessThan(5);
        }

        [Test]
        public void Returns_delegate()
        {
            Func<DateTime> func = SystemTime.Now;

            Assert.That(func, Is.Not.Null);
            Assert.That(func(), Is.TypeOf(typeof(DateTime)));
        }
    }
}