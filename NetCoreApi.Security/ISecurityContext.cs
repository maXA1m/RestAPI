using NetCoreApi.Data.Model;

namespace NetCoreApi.Security
{
    public interface ISecurityContext
    {
        User User { get; }

        bool IsAdministrator { get; }
    }
}
