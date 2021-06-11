using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Access.Extensions;

namespace NetCoreApi.Data.Access.DAL
{
    /// <summary>
    /// EF Context to acess database.
    /// </summary>
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CreateMappings();
            modelBuilder.Seed();
        }
    }
}
