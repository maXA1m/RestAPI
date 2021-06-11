using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Orders;
using NetCoreApi.Queries.Common;

namespace NetCoreApi.Queries
{
    /// <summary>
    /// Business logic for orders.
    /// In this realization looks like Repository, but it is more like Service/Manager.
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TCreateModel">Create model</typeparam>
    /// <typeparam name="TUpdateModel">Update model</typeparam>
    public class OrdersQueryProcessor : QueryProcessorBase<Order, CreateOrderModel, UpdateOrderModel>
    {
        public OrdersQueryProcessor(IUnitOfWork uow) : base(uow)
        {

        }

        protected override Order CreateItem(CreateOrderModel model)
        {
            return new Order
            {
                ItemId = model.ItemId,
                UserId = model.UserId,
                Quantity = model.Quantity,
                CreatedAt = model.CreatedAt
            };
        }

        protected override void UpdateItem(Order item, UpdateOrderModel model)
        {
            item.ItemId = model.ItemId;
            item.Quantity = model.Quantity;
        }
    }
}
