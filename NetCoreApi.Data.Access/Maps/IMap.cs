using Microsoft.EntityFrameworkCore;

namespace NetCoreApi.Data.Access.Maps
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}
