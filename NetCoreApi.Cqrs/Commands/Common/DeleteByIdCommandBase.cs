using System;
using System.Linq;
using System.Threading.Tasks;
using NetCoreApi.Common.Exceptions;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Commands.Common
{
    public abstract class DeleteByIdCommandBase<TModel> : ICommand<int, TModel>
        where TModel : EntityBase
    {
        private readonly IUnitOfWork _uow;

        public DeleteByIdCommandBase(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<TModel> ExecuteAsync(int parameter)
        {
            var item = _uow.Query<TModel>().FirstOrDefault(u => u.Id == parameter);
            if (item == null)
            {
                throw new NotFoundException($"{typeof(TModel)} is not found");
            }

            _uow.Remove(item);
            await _uow.CommitAsync();

            return item;
        }
    }
}
