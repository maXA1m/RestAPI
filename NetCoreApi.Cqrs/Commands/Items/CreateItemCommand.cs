using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;

namespace NetCoreApi.Cqrs.Commands.Items
{
    public class CreateItemCommand : CreateCommandBase<CreateItemModel, Item>
    {
        public CreateItemCommand(IUnitOfWork uow) : base(uow)
        {

        }

        protected override Item CreateModel(CreateItemModel parameter)
        {
            return new Item
            {
                Name = parameter.Name,
                AvailableQuantity = parameter.AvailableQuantity
            };
        }
    }
}
