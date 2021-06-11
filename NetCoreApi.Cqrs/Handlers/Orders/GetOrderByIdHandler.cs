using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Cqrs.Handlers.Orders
{
    public class GetOrderByIdHandler : GetByIdHandlerBase<OrderModel, Order>
    {
        public GetOrderByIdHandler(IQuery<int, Order> query, IMapper mapper) : base(query, mapper)
        {

        }
    }
}
