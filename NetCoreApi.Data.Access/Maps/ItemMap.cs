using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.Maps
{
    public class ItemMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Item>()
                .ToTable("Items")
                .HasKey(x => x.Id);
        }
    }
}
