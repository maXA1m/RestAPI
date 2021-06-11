using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Commands.Items
{
    public class DeleteItemByIdCommand : DeleteByIdCommandBase<Item>
    {
        public DeleteItemByIdCommand(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
