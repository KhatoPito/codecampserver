using System;
using July09v31.Core;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;

namespace July09v31.IntegrationTests.Infrastructure.DataAccess
{
    [TestFixture]
    public class UserGroupRepositoryTester : KeyedRepositoryTester<UserGroup, UserGroupRepository>
    {
        private static UserGroup CreateUserGroup()
        {
            var userGroup = new UserGroup
                            {
                                Name = "sdf",
                            };
            userGroup.Add(new User { EmailAddress = "werwer@asdfasd.com" });
            return userGroup;
        }


        [Test]
        public void Should_remove_a_user_from_its_collection()
        {
            UserGroup userGroup = CreateUserGroup();
            using (ISession session = GetSession())
            {
                userGroup.GetUsers().ForEach(o => session.SaveOrUpdate(o));
            }

            IUserGroupRepository repository = new UserGroupRepository(new HybridSessionBuilder());
            repository.Save(userGroup);
            userGroup.Remove(userGroup.GetUsers()[0]);
            repository.Save(userGroup);

            UserGroup rehydratedConference;
            using (ISession session = GetSession())
            {
                rehydratedConference = session.Load<UserGroup>(userGroup.Id);
                rehydratedConference.GetUsers().Length.ShouldEqual(0);
            }
        }

        [Test]
        public void Should_retrieve_the_default_usergroup()
        {
            UserGroup userGroup = CreateUserGroup();
            userGroup.Key = "localhost";

            using (ISession session = GetSession())
            {
                userGroup.GetUsers().ForEach(o => session.SaveOrUpdate(o));
                session.SaveOrUpdate(userGroup);
                session.Flush();
            }


            IUserGroupRepository repository = new UserGroupRepository(new HybridSessionBuilder());
            var group = repository.GetDefaultUserGroup();

            group.ShouldEqual(userGroup);
        }

        protected override UserGroupRepository CreateRepository()
        {
            return new UserGroupRepository(GetSessionBuilder());
        }
    }
}