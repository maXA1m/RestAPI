using System;
using System.Linq;
using System.Threading.Tasks;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Commands.Common
{
    public abstract class UpdateByIdCommandBase<TParameter, TModel> : ICommand<(int Id, TParameter Model), TModel>
        where TModel : EntityBase
    {
        protected readonly IUnitOfWork _uow;

        public UpdateByIdCommandBase(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<TModel> ExecuteAsync((int Id, TParameter Model) parameter)
        {
            var item = _uow.Query<TModel>().FirstOrDefault(i => i.Id == parameter.Id);
            if (item == null)
            {
                return null;
            }

            await _uow.CommitAsync();

            return item;
        }

        protected abstract void UpdateItem(TModel item, TParameter parameter);
    }
}
