using NetCoreApi.Cqrs.Queries.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Queries.Items
{
    public class GetAllItemsQuery : GetAllQueryBase<Item>
    {
        public GetAllItemsQuery(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
