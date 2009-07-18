using System;
using System.Linq;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using July09v31.Core.Services;
using July09v31.UI.Models.Forms;

namespace July09v31.UI.Helpers.Mappers
{
    public class UserMapper : AutoFormMapper<User, UserForm>, IUserMapper
    {
        private readonly ICryptographer _cryptographer;

        public UserMapper(IUserRepository repository, ICryptographer cryptographer)
            : base(repository)
        {
            _cryptographer = cryptographer;
        }

        protected override Guid GetIdFromMessage(UserForm form)
        {
            return form.Id;
        }

        protected override void MapToModel(UserForm form, User model)
        {
            model.Id = form.Id;
            model.Name = form.Name;
            model.EmailAddress = form.EmailAddress;
            model.PasswordSalt = _cryptographer.CreateSalt();
            model.PasswordHash = _cryptographer.GetPasswordHash(form.Password,
                                                                model.PasswordSalt);
            model.Username = form.Username;
        }

        public UserForm[] Map(User[] model)
        {
            return model.Select(user => Map(user)).ToArray();
        }

        public User[] Map(UserForm[] message)
        {
            return message.Select(form => Map(form)).ToArray();
        }
    }
}