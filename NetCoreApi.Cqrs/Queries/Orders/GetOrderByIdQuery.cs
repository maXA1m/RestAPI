using NetCoreApi.Cqrs.Queries.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Queries.Orders
{
    public class GetOrderByIdQuery : GetByIdQueryBase<Order>
    {
        public GetOrderByIdQuery(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
