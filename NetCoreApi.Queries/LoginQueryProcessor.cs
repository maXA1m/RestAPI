using System;
using System.Linq;
using System.Threading.Tasks;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Login;
using NetCoreApi.Model.Users;
using NetCoreApi.Queries.Common;
using NetCoreApi.Security;
using NetCoreApi.Security.Token;

namespace NetCoreApi.Queries
{
    public class LoginQueryProcessor : ILoginQueryProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IQueryProcessor<User, CreateUserModel, UpdateUserModel> _users;
        private readonly ISecurityContext _context;

        public LoginQueryProcessor(IUnitOfWork uow, ITokenBuilder tokenBuilder, IQueryProcessor<User, CreateUserModel, UpdateUserModel> users, ISecurityContext context)
        {
            _uow = uow;
            _tokenBuilder = tokenBuilder;
            _users = users;
            _context = context;
        }

        public UserWithToken Authenticate(string username, string password)
        {
            var user = _users.Get().Where(u => u.Username == username).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(password) || !BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
            {
                return null;
            }

            var expiresIn = DateTime.Now + TokenOptions.ExpiresSpan;
            var token = _tokenBuilder.Build(user.Username, user.Roles.Select(x => x.Role.Name).ToArray(), expiresIn);

            return new UserWithToken
            {
                ExpiresAt = expiresIn,
                Token = token,
                User = user
            };
        }

        public async Task<User> Register(RegisterModel model)
        {
            var requestModel = new CreateUserModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username
            };

            var user = await _users.Create(requestModel);
            return user;
        }

        public async Task ChangePassword(ChangeUserPasswordModel requestModel)
        {
            var user = _users.Get(_context.User.Id);
            user.Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password.Trim());
            await _uow.CommitAsync();
        }
    }
}
