using NetCoreApi.Cqrs.Queries.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Queries.Items
{
    public class GetItemByIdQuery : GetByIdQueryBase<Item>
    {
        public GetItemByIdQuery(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
