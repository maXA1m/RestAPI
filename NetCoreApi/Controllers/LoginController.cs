using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Filters;
using NetCoreApi.Model.Login;
using NetCoreApi.Model.Users;
using NetCoreApi.Queries;

namespace NetCoreApi.Controllers
{
    /// <summary>
    /// Provides API to access auth.
    /// Classic N-Layer with Services/Managers needs to inject many dependencies.
    /// </summary>
    [Route("api/v1/login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILoginQueryProcessor _query;
        private readonly IMapper _mapper;

        public LoginController(ILoginQueryProcessor query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpPost("Authenticate")]
        [ValidateModel]
        public UserWithTokenModel Authenticate([FromBody] LoginModel model)
        {
            var result = _query.Authenticate(model.Username, model.Password);
            var resultModel = _mapper.Map<UserWithTokenModel>(result);

            return resultModel;
        }

        [HttpPost("Register")]
        [ValidateModel]
        public async Task<UserModel> Register([FromBody] RegisterModel model)
        {
            var result = await _query.Register(model);
            var resultModel = _mapper.Map<UserModel>(result);

            return resultModel;
        }

        [HttpPost("Password")]
        [ValidateModel]
        [Authorize]
        public async Task ChangePassword([FromBody] ChangeUserPasswordModel requestModel)
        {
            await _query.ChangePassword(requestModel);
        }
    }
}
