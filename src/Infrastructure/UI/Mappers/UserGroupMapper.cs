using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.UI.Mappers
{
	public class UserGroupMapper : AutoInputMapper<UserGroup, UserGroupInput>, IUserGroupMapper,
	                               ITypeConverter<string, UserGroup>, ITypeConverter<Guid, UserGroup>
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserRepository _userRepository;

		public UserGroupMapper(IUserGroupRepository repository, IUserRepository userRepository) : base(repository)
		{
			_repository = repository;
			_userRepository = userRepository;
		}

		protected override Guid GetIdFromMessage(UserGroupInput input)
		{
			return input.Id;
		}

		protected override void MapToModel(UserGroupInput input, UserGroup model)
		{
			model.DomainName = input.DomainName;
			model.Key = input.Key;
			model.Id = input.Id;
			model.Name = input.Name;
			model.HomepageHTML = input.HomepageHTML;
			model.City = input.City;
			model.Region = input.Region;
			model.Country = input.Country;
			model.GoogleAnalysticsCode = input.GoogleAnalysticsCode;
			User[] existingUsers = model.GetUsers();

			IEnumerable<User> usersToRemove = existingUsers.Where(user => !input.Users.Any(uf => uf.Id == user.Id));

			foreach (User user in usersToRemove)
			{
				model.Remove(user);
			}

			IEnumerable<UserSelectorInput> userFormToAdd =
				input.Users.Where(userForm => !existingUsers.Any(user => user.Id == userForm.Id));
			User[] users = _userRepository.GetAll();

			foreach (UserSelectorInput userForm in userFormToAdd)
			{
				User user = users.FirstOrDefault(user1 => user1.Id == userForm.Id);
				model.Add(user);
			}
		}

		public UserGroup Convert(string source)
		{
			return _repository.GetByKey(source);
		}

		public UserGroup Convert(Guid source)
		{
			return _repository.GetById(source);
		}
	}
}