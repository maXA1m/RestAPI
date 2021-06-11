using NetCoreApi.Cqrs.Commands.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Commands.Users
{
    public class DeleteUserByIdCommand : DeleteByIdCommandBase<User>
    {
        public DeleteUserByIdCommand(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
