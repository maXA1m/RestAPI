using System;
using System.Threading.Tasks;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Commands.Common
{
    public abstract class CreateCommandBase<TParameter, TModel> : ICommand<TParameter, TModel>
        where TModel : EntityBase
    {
        protected readonly IUnitOfWork _uow;

        public CreateCommandBase(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<TModel> ExecuteAsync(TParameter parameter)
        {
            var item = CreateModel(parameter);

            _uow.Add(item);

            await _uow.CommitAsync();

            return item;
        }

        protected abstract TModel CreateModel(TParameter parameter);
    }
}
