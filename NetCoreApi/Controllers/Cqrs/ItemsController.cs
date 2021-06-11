using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Filters;
using NetCoreApi.Model;
using NetCoreApi.Model.Items;
using NetCoreApi.Security;

namespace NetCoreApi.Controllers.Cqrs
{
    /// <summary>
    /// Provides API to access items.
    /// Clean controller without injections with CQRS pattern.
    /// </summary>
    [ApiController]
    [Route("api/v1/cqrs/items")]
    [AllowAnonymous]
    public class ItemsController : Controller
    {
        [HttpGet]
        public IEnumerable<ItemModel> Get([FromServices] IHandler<PaginationOptions, IQueryable<ItemModel>> handler, [FromQuery] PaginationOptions paginaton = null)
        {
            return handler.Handle(paginaton);
        }

        [HttpGet("{id}")]
        public ActionResult<ItemModel> Get(int id, [FromServices] IHandler<int, ItemModel> handler)
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
        [Authorize(Roles = Roles.AdministratorOrManager)]
        public async Task<ItemModel> Post([FromBody] CreateItemModel requestModel, [FromServices] IHandler<CreateItemModel, Task<ItemModel>> handler)
        {
            return await handler.Handle(requestModel);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = Roles.AdministratorOrManager)]
        public async Task<ActionResult<ItemModel>> Put(int id, [FromBody] UpdateItemModel requestModel, [FromServices] IHandler<(int Id, UpdateItemModel Model), Task<ItemModel>> handler)
        {
            var item = await handler.Handle((id, requestModel));
            if (item == null)
            {
                return BadRequest(new ArgumentNullException(nameof(id)));
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdministratorOrManager)]
        public async Task<ActionResult> Delete(int id, [FromServices] IHandler<int, Task<ItemModel>> handler)
        {
            if (await handler.Handle(id) != null)
            {
                return Ok();
            }

            return BadRequest(new ArgumentOutOfRangeException(nameof(id)));
        }
    }
}
