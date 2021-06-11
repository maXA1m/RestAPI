using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Common.Exceptions;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Users;
using NetCoreApi.Queries.Common;

namespace NetCoreApi.Queries
{
    /// <summary>
    /// Business logic for users.
    /// In this realization looks like Repository, but it is more like Service/Manager.
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TCreateModel">Create model</typeparam>
    /// <typeparam name="TUpdateModel">Update model</typeparam>
    public class UsersQueryProcessor : QueryProcessorBase<User, CreateUserModel, UpdateUserModel>
    {
        public UsersQueryProcessor(IUnitOfWork uow) : base(uow)
        {

        }

        protected override IQueryable<User> GetQuery()
        {
            return _uow.Query<User>()
                    .Include(x => x.Roles)
                    .ThenInclude(x => x.Role);
        }

        protected override User CreateItem(CreateUserModel model)
        {
            var username = model.Username.Trim();
            if (Get().Any(u => u.Username == username))
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
