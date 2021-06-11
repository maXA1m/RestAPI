using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;

namespace NetCoreApi.Cqrs.Commands.Items
{
    public class UpdateItemByIdCommand : UpdateByIdCommandBase<UpdateItemModel, Item>
    {
        public UpdateItemByIdCommand(IUnitOfWork uow) : base(uow)
        {

        }

        protected override void UpdateItem(Item item, UpdateItemModel parameter)
        {
            item.Name = parameter.Name;
            item.AvailableQuantity = parameter.AvailableQuantity;
        }
    }
}
