using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Filters;
using NetCoreApi.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Controllers.Cqrs
{
    /// <summary>
    /// Provides API to access orders.
    /// Clean controller without injections with CQRS pattern.
    /// </summary>
    [ApiController]
    [Route("api/v1/cqrs/orders")]
    [Authorize]
    public class OrdersController : Controller
    {
        [HttpGet]
        public IEnumerable<OrderModel> Get([FromServices] IHandler<PaginationOptions, IQueryable<OrderModel>> handler, [FromQuery] PaginationOptions paginaton = null)
        {
            return handler.Handle(paginaton);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderModel> Get(int id, [FromServices] IHandler<int, OrderModel> handler)
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
        public async Task<OrderModel> Post([FromBody] CreateOrderModel requestModel, [FromServices] IHandler<CreateOrderModel, Task<OrderModel>> handler)
        {
            return await handler.Handle(requestModel);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<OrderModel>> Put(int id, [FromBody] UpdateOrderModel requestModel, [FromServices] IHandler<(int Id, UpdateOrderModel Model), Task<OrderModel>> handler)
        {
            var item = await handler.Handle((id, requestModel));
            if (item == null)
            {
                return BadRequest(new ArgumentNullException(nameof(id)));
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromServices] IHandler<int, Task<OrderModel>> handler)
        {
            if (await handler.Handle(id) != null)
            {
                return Ok();
            }

            return BadRequest(new ArgumentOutOfRangeException(nameof(id)));
        }
    }
}
