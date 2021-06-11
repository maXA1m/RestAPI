using NetCoreApi.Cqrs.Queries.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Queries.Orders
{
    public class GetAllOrdersQuery : GetAllQueryBase<Order>
    {
        public GetAllOrdersQuery(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
