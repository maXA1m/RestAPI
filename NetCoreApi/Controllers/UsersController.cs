using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Data.Model;
using NetCoreApi.Filters;
using NetCoreApi.Model;
using NetCoreApi.Model.Users;
using NetCoreApi.Queries.Common;
using NetCoreApi.Security;

namespace NetCoreApi.Controllers
{
    /// <summary>
    /// Provides API to access users.
    /// Classic N-Layer with Services/Managers needs to inject many dependencies.
    /// </summary>
    [ApiController]
    [Route("api/v1/users")]
    [Authorize(Roles = Roles.AdministratorOrManager)]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQueryProcessor<User, CreateUserModel, UpdateUserModel> _query;

        public UsersController(IQueryProcessor<User, CreateUserModel, UpdateUserModel> query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<UserModel> Get([FromQuery] PaginationOptions paginaton = null)
        {
            return _query.Get(paginaton).ProjectTo<UserModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return _mapper.Map<UserModel>(_query.Get(id));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<UserModel> Post([FromBody] CreateUserModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<UserModel>(item);

            return model;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<UserModel> Put(int id, [FromBody] UpdateUserModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<UserModel>(item);

            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
