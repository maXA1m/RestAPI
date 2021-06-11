using System.Linq;
using NetCoreApi.Common.Exceptions;
using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Users;

namespace NetCoreApi.Cqrs.Commands.Users
{
    public class CreateUserCommand : CreateCommandBase<CreateUserModel, User>
    {
        public CreateUserCommand(IUnitOfWork uow) : base(uow)
        {

        }

        protected override User CreateModel(CreateUserModel model)
        {
            var username = model.Username.Trim();
            if (_uow.Query<User>().Any(u => u.Username == username))
            {
                throw new System.Exception("The username is already in use");
            }

            var user = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password.Trim()),
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
            };

            AddUserRoles(user, model.Roles);

            return user;
        }


        private void AddUserRoles(User user, string[] roles)
        {
            user.Roles.Clear();

            foreach (var roleName in roles)
            {
                var role = _uow.Query<Role>().FirstOrDefault(x => x.Name == roleName);

                if (role == null)
                {
                    throw new NotFoundException($"Role - {roleName} is not found");
                }

                user.Roles.Add(new UserRole { User = user, Role = role });
            }
        }
    }
}
