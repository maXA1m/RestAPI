using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;
using NetCoreApi.Queries.Common;

namespace NetCoreApi.Queries
{
    /// <summary>
    /// Business logic for items.
    /// In this realization looks like Repository, but it is more like Service/Manager.
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TCreateModel">Create model</typeparam>
    /// <typeparam name="TUpdateModel">Update model</typeparam>
    public class ItemsQueryProcessor : QueryProcessorBase<Item, CreateItemModel, UpdateItemModel>
    {
        public ItemsQueryProcessor(IUnitOfWork uow) : base(uow)
        {

        }

        protected override Item CreateItem(CreateItemModel model)
        {
            return new Item
            {
                Name = model.Name,
                AvailableQuantity = model.AvailableQuantity
            };
        }

        protected override void UpdateItem(Item item, UpdateItemModel model)
        {
            item.Name = model.Name;
            item.AvailableQuantity = model.AvailableQuantity;
        }
    }
}
