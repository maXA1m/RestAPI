using System;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCoreApi.Data.Access.DAL;

namespace NetCoreApi.Helpers
{
    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private IUnitOfWork _uow;
        private ITransaction _transaction;

        public ActionTransactionHelper(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void BeginTransaction()
        {
            _transaction = _uow.BeginTransaction();
        }

        public void EndTransaction(ActionExecutedContext filterContext)
        {
            if (_transaction == null) throw new NotSupportedException();
            if (filterContext.Exception == null)
            {
                _uow.Commit();
                _transaction.Commit();
            }
            else
            {
                try
                {
                    _transaction.Rollback();
                }
                catch (Exception ex)
                {
                    throw new AggregateException(filterContext.Exception, ex);
                }
            }
        }

        public void CloseSession()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_uow != null)
            {
                _uow.Dispose();
                _uow = null;
            }
        }
    }
}
