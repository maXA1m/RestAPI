using AutoMapper;
using NetCoreApi.Model.Orders;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Cqrs.Common;

namespace NetCoreApi.Cqrs.Handlers.Orders
{
    public class UpdateOrderByIdHandler : UpdateByIdHandlerBase<UpdateOrderModel, OrderModel, Order>
    {
        public UpdateOrderByIdHandler(ICommand<(int Id, UpdateOrderModel Model), Order> command, IMapper mapper) : base(command, mapper)
        {

        }
    }
}
