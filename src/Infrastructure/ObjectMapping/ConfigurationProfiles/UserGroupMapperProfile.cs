using System;
using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles
{
	public class UserGroupMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<UserGroup, UserGroupInput>();
			Mapper.CreateMap<string, UserGroup>().ConvertUsing<UserGroupMapper>();
			Mapper.CreateMap<Guid, UserGroup>().ConvertUsing<UserGroupMapper>();
		}
	}
}