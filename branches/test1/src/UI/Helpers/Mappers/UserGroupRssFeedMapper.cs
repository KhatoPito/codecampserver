using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Helpers.Mappers
{
    public class UserGroupRssFeedMapper : AutoFormMapper<UserGroup, RssFeedForm>, IUserGroupRssFeedMapper
    {
        public UserGroupRssFeedMapper(IRepository<UserGroup> repository) : base(repository) { }

        protected override Guid GetIdFromMessage(RssFeedForm message)
        {
            return message.ParentID;
        }

        protected override void MapToModel(RssFeedForm message, UserGroup model)
        {
            throw new NotImplementedException();

        }
    }
}
