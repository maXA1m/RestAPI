using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Filters;
using NetCoreApi.Model;
using NetCoreApi.Model.Users;
using NetCoreApi.Security;

namespace NetCoreApi.Controllers.Cqrs
{
    /// <summary>
    /// Provides API to access users.
    /// Clean controller without injections with CQRS pattern.
    /// </summary>
    [ApiController]
    [Route("api/v1/cqrs/users")]
    [Authorize(Roles = Roles.AdministratorOrManager)]
    public class UsersController : Controller
    {
        [HttpGet]
        public IEnumerable<UserModel> Get([FromServices] IHandler<PaginationOptions, IQueryable<UserModel>> handler, [FromQuery] PaginationOptions paginaton = null)
        {
            return handler.Handle(paginaton);
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(int id, [FromServices] IHandler<int, UserModel> handler)
        {
            var item = handler.Handle(id);
            if (item == null)
            {
                return BadRequest(new ArgumentNullException(nameof(id)));
            }

            return Ok(item);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<UserModel> Post([FromBody] CreateUserModel requestModel, [FromServices] IHandler<CreateUserModel, Task<UserModel>> handler)
        {
            return await handler.Handle(requestModel);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<UserModel>> Put(int id, [FromBody] UpdateUserModel requestModel, [FromServices] IHandler<(int Id, UpdateUserModel Model), Task<UpdateUserModel>> handler)
        {
            var item = await handler.Handle((id, requestModel));
            if (item == null)
            {
                return BadRequest(new ArgumentNullException(nameof(id)));
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromServices] IHandler<int, Task<UpdateUserModel>> handler)
        {
            if (await handler.Handle(id) != null)
            {
                return Ok();
            }

            return BadRequest(new ArgumentOutOfRangeException(nameof(id)));
        }
    }
}
