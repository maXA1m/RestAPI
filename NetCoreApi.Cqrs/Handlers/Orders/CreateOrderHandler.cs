using AutoMapper;
using NetCoreApi.Model.Orders;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Cqrs.Common;

namespace NetCoreApi.Cqrs.Handlers.Orders
{
    public class CreateOrderHandler : CreateHandlerBase<CreateOrderModel, OrderModel, Order>
    {
        public CreateOrderHandler(ICommand<CreateOrderModel, Order> command, IMapper mapper) : base(command, mapper)
        {

        }
    }
}
