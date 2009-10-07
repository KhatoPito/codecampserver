using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup;
using CodeCampServer.Infrastructure.UI.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
{
	public class UserGroupMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<UserGroup, UserGroupInput>().ForMember(m => m.Users,
			                                                        c => c.ResolveUsing<UserToUserSelectorInputTypeConverter>());
			Mapper.CreateMap<string, UserGroup>().ConvertUsing<UserGroupMapper>();
			Mapper.CreateMap<Guid, UserGroup>().ConvertUsing<UserGroupMapper>();
			Mapper.CreateMap<UserGroupInput, UserGroup>().ConvertUsing<UserGroupInputToUserGroupTypeConverter>();

			Mapper.CreateMap<UserGroupInput, UpdateUserGroupCommandMessage>()
				.ConvertUsing(input => new UpdateUserGroupCommandMessage
				                       	{
				                       		UserGroup = Mapper.Map<UserGroupInput, UserGroup>(input)
				                       	});
		}
	}

	public class UserToUserSelectorInputTypeConverter : IValueResolver {
		public ResolutionResult Resolve(ResolutionResult source)
		{
			User[] users = (User[]) source.Value;

			var inputs =users.Select(user => Mapper.Map<User, UserInput>(user)).ToList();

			return
				new ResolutionResult( inputs );
		}
	}
}