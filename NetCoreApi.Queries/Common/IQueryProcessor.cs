using System.Linq;
using System.Threading.Tasks;
using NetCoreApi.Data.Model;
using NetCoreApi.Model;

namespace NetCoreApi.Queries.Common
{
    /// <summary>
    /// Business logic for single entity/table.
    /// In this realization looks like Repository, but it is more like Service/Manager.
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TCreateModel">Create model</typeparam>
    /// <typeparam name="TUpdateModel">Update model</typeparam>
    public interface IQueryProcessor<TEntity, in TCreateModel, in TUpdateModel>
        where TEntity : EntityBase
    {
        IQueryable<TEntity> Get(PaginationOptions pagination = null);

        TEntity Get(int id);

        Task<TEntity> Create(TCreateModel model);

        Task<TEntity> Update(int id, TUpdateModel model);

        Task Delete(int id);
    }
}
