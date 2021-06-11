using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Commands.Orders
{
    public class DeleteOrderByIdCommand : DeleteByIdCommandBase<Order>
    {
        public DeleteOrderByIdCommand(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
