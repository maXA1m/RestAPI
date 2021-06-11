using AutoMapper;
using NetCoreApi.Model.Items;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Cqrs.Common;

namespace NetCoreApi.Cqrs.Handlers.Items
{
    public class UpdateItemByIdHandler : UpdateByIdHandlerBase<UpdateItemModel, ItemModel, Item>
    {
        public UpdateItemByIdHandler(ICommand<(int Id, UpdateItemModel Model), Item> command, IMapper mapper)
            : base(command, mapper)
        {

        }
    }
}
