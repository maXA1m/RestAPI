using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.DAL
{
    /// <summary>
    /// UnitOfWork pattern to encapsulate DB context.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        void Add<T>(T item) where T : EntityBase;

        void Update<T>(T item) where T : EntityBase;

        void Remove<T>(T item) where T : EntityBase;

        IQueryable<T> Query<T>() where T : EntityBase;

        void Commit();

        Task CommitAsync();

        void Attach<T>(T item) where T : EntityBase;
    }
}
