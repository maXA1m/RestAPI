using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.Maps
{
    public class OrderMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(x => x.Id);
        }
    }
}
