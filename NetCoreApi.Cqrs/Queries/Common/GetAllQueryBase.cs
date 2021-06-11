using System;
using System.Linq;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model;

namespace NetCoreApi.Cqrs.Queries.Common
{
    public abstract class GetAllQueryBase<TModel> : IQuery<PaginationOptions, IQueryable<TModel>>
        where TModel : EntityBase
    {
        protected readonly IUnitOfWork _uow;

        public GetAllQueryBase(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public IQueryable<TModel> Execute(PaginationOptions parameter)
        {
            if (parameter == null)
            {
                return Get();
            }

            return Get().Skip(parameter.Offset).Take(parameter.Count);
        }

        protected virtual IQueryable<TModel> Get()
        {
            return _uow.Query<TModel>();
        }
    }
}
