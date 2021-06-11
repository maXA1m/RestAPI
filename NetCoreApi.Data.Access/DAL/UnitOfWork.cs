using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.DAL
{
    /// <summary>
    /// Implementation of UnitOfWork pattern to encapsulate DB context.
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public EFUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return new Transaction(_context.Database.BeginTransaction(isolationLevel));
        }

        public void Add<T>(T item)
            where T : EntityBase
        {
            var set = _context.Set<T>();
            set.Add(item);
        }

        public void Update<T>(T item)
            where T : EntityBase
        {
            Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        void IUnitOfWork.Remove<T>(T item)
        {
            var set = _context.Set<T>();
            set.Remove(item);
        }

        public IQueryable<T> Query<T>()
            where T : EntityBase
        {
            return _context.Set<T>();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Attach<T>(T item) where T : EntityBase
        {
            var set = _context.Set<T>();
            set.Attach(item);
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}
