using System.Web.Mvc;
using July09v31.Core.Domain.Model;
using July09v31.UI.Controllers;
using July09v31.UI.Helpers.Mappers;
using July09v31.UI.Models.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using NBehave.Spec.NUnit;

namespace July09v31.UnitTests.UI.Controllers
{
    [TestFixture]
    public class ScheduleControllerTester : TestBase
    {
        [Test]
        public void Should_map_schedule_and_display()
        {
            var conference = new Conference();
            var scheduleForms = new ScheduleForm[0];

            var mapper = S<IScheduleMapper>();
            mapper.Stub(x => x.Map(conference)).Return(scheduleForms);

            var controller = new ScheduleController(mapper);
            ViewResult result = controller.Index(conference);
            result.ViewName.ShouldEqual("");
            result.ViewData.Model.ShouldEqual(scheduleForms);
        }
    }
}