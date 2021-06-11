using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreApi.Helpers
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();

        void EndTransaction(ActionExecutedContext filterContext);

        void CloseSession();
    }
}