using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.Maps
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);
        }
    }
}
