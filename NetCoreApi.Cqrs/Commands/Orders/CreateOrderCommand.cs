using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Cqrs.Commands.Orders
{
    public class CreateOrderCommand : CreateCommandBase<CreateOrderModel, Order>
    {
        public CreateOrderCommand(IUnitOfWork uow) : base(uow)
        {

        }

        protected override Order CreateModel(CreateOrderModel parameter)
        {
            return new Order
            {
                UserId = parameter.UserId,
                ItemId = parameter.ItemId,
                Quantity = parameter.Quantity
            };
        }
    }
}
