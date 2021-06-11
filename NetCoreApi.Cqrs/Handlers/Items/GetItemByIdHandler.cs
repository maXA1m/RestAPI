using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;

namespace NetCoreApi.Cqrs.Handlers.Items
{
    public class GetItemByIdHandler : GetByIdHandlerBase<ItemModel, Item>
    {
        public GetItemByIdHandler(IQuery<int, Item> query, IMapper mapper) : base(query, mapper)
        {

        }
    }
}
