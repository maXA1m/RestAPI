using System;
using System.Linq;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Queries.Common
{
    public abstract class GetByIdQueryBase<TModel> : IQuery<int, TModel>
        where TModel : EntityBase
    {
        protected readonly IUnitOfWork _uow;

        public GetByIdQueryBase(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public TModel Execute(int parameter)
        {
            return Get().FirstOrDefault(i => i.Id == parameter);
        }

        protected virtual IQueryable<TModel> Get()
        {
            return _uow.Query<TModel>();
        }
    }
}
