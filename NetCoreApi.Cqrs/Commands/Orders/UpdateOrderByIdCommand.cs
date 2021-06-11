using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Cqrs.Commands.Orders
{
    public class UpdateOrderByIdCommand : UpdateByIdCommandBase<UpdateOrderModel, Order>
    {
        public UpdateOrderByIdCommand(IUnitOfWork uow) : base(uow)
        {

        }

        protected override void UpdateItem(Order item, UpdateOrderModel parameter)
        {
            item.ItemId = parameter.ItemId;
            item.Quantity = parameter.Quantity;
        }
    }
}
