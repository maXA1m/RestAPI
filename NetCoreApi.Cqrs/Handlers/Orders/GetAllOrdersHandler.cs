using System.Linq;
using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Cqrs.Handlers.Orders
{
    public class GetAllOrdersHandler : GetAllHandlerBase<OrderModel, Order>
    {
        public GetAllOrdersHandler(IQuery<PaginationOptions, IQueryable<Order>> query, IMapper mapper) : base(query, mapper)
        {

        }
    }
}
