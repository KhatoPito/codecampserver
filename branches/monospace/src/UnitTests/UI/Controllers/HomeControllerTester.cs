using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using CodeCampServer.Core.Domain;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class HomeControllerTester : SaveControllerTester
	{
		[Test]
		public void About_should_go_to_about_view()
		{
			var home = new HomeController(S<IMeetingRepository>(), S<IMeetingMapper>());

			ViewResult result = home.About();
			result.ViewName.ShouldEqual("");
		}

		[Test]
		public void The_index_should_retrieve_the_user_group_by_its_domain_name()
		{
			var home = new HomeController(S<IMeetingRepository>(), S<IMeetingMapper>());

			ViewResult result = home.Index();
			result.ForView("");
			result.WithViewData<HomeDisplay>().ShouldNotBeNull();
		}
	}
}