using System;

namespace NetCoreApi.Data.Access.DAL
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
