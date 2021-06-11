using System.Linq;
using NetCoreApi.Common.Exceptions;
using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Users;

namespace NetCoreApi.Cqrs.Commands.Users
{
    public class UpdateUserByIdCommand : UpdateByIdCommandBase<UpdateUserModel, User>
    {
        public UpdateUserByIdCommand(IUnitOfWork uow) : base(uow)
        {

        }

        protected override void UpdateItem(User item, UpdateUserModel model)
        {
            item.Username = model.Username;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            AddUserRoles(item, model.Roles);
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
