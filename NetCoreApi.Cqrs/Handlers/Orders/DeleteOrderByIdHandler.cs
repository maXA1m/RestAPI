using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Cqrs.Handlers.Orders
{
    public class DeleteOrderByIdHandler : DeleteByIdHandlerBase<OrderModel, Order>
    {
        public DeleteOrderByIdHandler(ICommand<int, Order> command, IMapper mapper) : base(command, mapper)
        {

        }
    }
}
