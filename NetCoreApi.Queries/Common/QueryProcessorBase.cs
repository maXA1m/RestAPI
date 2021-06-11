using System.Linq;
using System.Threading.Tasks;
using NetCoreApi.Common.Exceptions;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model;

namespace NetCoreApi.Queries.Common
{
    /// <summary>
    /// Abstract class that contains base business logic for single entity/table.
    /// In this realization looks like Repository, but it is more like Service/Manager.
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TCreateModel">Create model</typeparam>
    /// <typeparam name="TUpdateModel">Update model</typeparam>
    public abstract class QueryProcessorBase<TEntity, TCreateModel, TUpdateModel> : IQueryProcessor<TEntity, TCreateModel, TUpdateModel>
        where TEntity : EntityBase
    {
        protected readonly IUnitOfWork _uow;

        public QueryProcessorBase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IQueryable<TEntity> Get(PaginationOptions pagination = null)
        {
            var items = GetQuery();
            if (pagination == null)
            {
                return items;
            }

            return items.Skip(pagination.Offset).Take(pagination.Count);
        }

        protected virtual IQueryable<TEntity> GetQuery()
        {
            return _uow.Query<TEntity>();
        }

        public TEntity Get(int id)
        {
            var item = Get().FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                throw new NotFoundException($"{nameof(TEntity)} is not found");
            }

            return item;
        }

        public async Task<TEntity> Create(TCreateModel model)
        {
            var item = CreateItem(model);

            _uow.Add(item);
            await _uow.CommitAsync();

            return item;
        }

        protected abstract TEntity CreateItem(TCreateModel model);

        public async Task<TEntity> Update(int id, TUpdateModel model)
        {
            var item = Get().FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                throw new NotFoundException($"{nameof(TEntity)} is not found");
            }

            UpdateItem(item, model);

            await _uow.CommitAsync();
            return item;
        }

        protected abstract void UpdateItem(TEntity item, TUpdateModel model);

        public async Task Delete(int id)
        {
            var item = Get().FirstOrDefault(u => u.Id == id);
            if (item == null)
            {
                throw new NotFoundException($"{nameof(TEntity)} is not found");
            }

            _uow.Remove(item);
            await _uow.CommitAsync();
        }
    }
}
