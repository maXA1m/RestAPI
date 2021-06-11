using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Users;
using NetCoreApi.Queries.Common;

namespace NetCoreApi.Security
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IQueryProcessor<User, CreateUserModel, UpdateUserModel> _users;
        private User _user;

        public SecurityContext(IHttpContextAccessor contextAccessor, IQueryProcessor<User, CreateUserModel, UpdateUserModel> users)
        {
            _contextAccessor = contextAccessor;
            _users = users;
        }

        public User User
        {
            get
            {
                if (_user != null)
                {
                    return _user;
                }

                if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException();
                }

                var username = _contextAccessor.HttpContext.User.Identity.Name;
                _user = _users.Get()
                    .Where(x => x.Username == username)
                    .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                    .FirstOrDefault();

                return _user ?? throw new UnauthorizedAccessException("User is not found");
            }
        }

        public bool IsAdministrator => User.Roles.Any(x => x.Role.Name == Roles.Administrator);
    }
}
