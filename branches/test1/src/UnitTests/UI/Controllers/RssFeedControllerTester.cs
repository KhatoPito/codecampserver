using System;
using System.Web.Mvc;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.Core.Services;
using July09v31.Infrastructure.UI.Services.Impl;
using July09v31.UI.Controllers;
using July09v31.UI.Helpers.Mappers;
using July09v31.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace July09v31.UnitTests.UI.Controllers
{
    public class RssFeedControllerTester : SaveControllerTester
    {
        [Test]
        public void Should_list_the_sponors_for_a_user_group()
        {
            var controller = new RssFeedController(S<IUserGroupRepository>(), S<IUserGroupRssFeedMapper>(), PermisiveSecurityContext());

            controller.Index(new UserGroup())

            .AssertViewRendered()
            .ForView(ViewNames.Default)
            .ModelShouldBe<RssFeedForm[]>();
        }

        [Test]
        public void Should_add_a_RssFeed_for_a_user_group()
        {
            var controller = new RssFeedController(S<IUserGroupRepository>(), S<IUserGroupRssFeedMapper>(), PermisiveSecurityContext());

            controller.New(new UserGroup())

            .AssertViewRendered()
            .ForView(ViewNames.Edit)
            .ModelShouldBe<RssFeedForm>();

        }

        [Test]
        public void Should_edit_an_existing_RssFeed()
        {
            var controller = new RssFeedController(S<IUserGroupRepository>(), S<IUserGroupRssFeedMapper>(), PermisiveSecurityContext());

            var userGroup = new UserGroup();
            userGroup.Add(new RssFeed() { Id = Guid.Empty });

            controller.Edit(userGroup, Guid.Empty)

            .AssertViewRendered()
            .ForView(ViewNames.Default)
            .ModelShouldBe<RssFeedForm>();

        }

        [Test]
        public void Should_save_a_new_RssFeed_in_the_Save_action()
        {
            var userGroup = new UserGroup();

            var repository = S<IUserGroupRepository>();
            var controller = new RssFeedController(repository, S<IUserGroupRssFeedMapper>(), PermisiveSecurityContext());

            controller.Save(userGroup, new RssFeedForm())

                .AssertActionRedirect()
                .ToAction<RssFeedController>(c => c.Index(userGroup));
        }

        [Test]
        public void Should_delete_a_RssFeed_from_the_delete_action()
        {
            var repository = S<IUserGroupRepository>();
            var controller = new RssFeedController(repository, S<IUserGroupRssFeedMapper>(), PermisiveSecurityContext());

            var userGroup = new UserGroup();

            controller.Delete(userGroup, new RssFeed())
                .AssertActionRedirect()
                .ToAction<RssFeedController>(c => c.Index(null));

            repository.AssertWasCalled(x => x.Save(userGroup));
        }
    }
}