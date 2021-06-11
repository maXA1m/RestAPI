using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;

namespace NetCoreApi.Cqrs.Handlers.Items
{
    public class DeleteItemByIdHandler : DeleteByIdHandlerBase<ItemModel, Item>
    {
        public DeleteItemByIdHandler(ICommand<int, Item> command, IMapper mapper) : base(command, mapper)
        {

        }
    }
}
