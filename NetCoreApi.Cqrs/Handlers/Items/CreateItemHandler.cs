using AutoMapper;
using NetCoreApi.Model.Items;
using NetCoreApi.Data.Model;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Cqrs.Common;

namespace NetCoreApi.Cqrs.Handlers.Items
{
    public class CreateItemHandler : CreateHandlerBase<CreateItemModel, ItemModel, Item>
    {
        public CreateItemHandler(ICommand<CreateItemModel, Item> command, IMapper mapper) : base(command, mapper)
        {

        }
    }
}
