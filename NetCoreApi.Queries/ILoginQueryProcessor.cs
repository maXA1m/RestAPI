using System.Threading.Tasks;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Login;
using NetCoreApi.Model.Users;

namespace NetCoreApi.Queries
{
    public interface ILoginQueryProcessor
    {
        UserWithToken Authenticate(string username, string password);

        Task<User> Register(RegisterModel model);

        Task ChangePassword(ChangeUserPasswordModel requestModel);
    }
}
