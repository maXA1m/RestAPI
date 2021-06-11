using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.Maps
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(x => x.Id);
        }
    }
}
