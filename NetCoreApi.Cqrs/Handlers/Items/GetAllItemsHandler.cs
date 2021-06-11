using System.Linq;
using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Cqrs.Handlers.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model;
using NetCoreApi.Model.Items;

namespace NetCoreApi.Cqrs.Handlers.Items
{
    public class GetAllItemsHandler : GetAllHandlerBase<ItemModel, Item>
    {
        public GetAllItemsHandler(IQuery<PaginationOptions, IQueryable<Item>> query, IMapper mapper) : base(query, mapper)
        {

        }
    }
}
