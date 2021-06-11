using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Access.Maps;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Data.Access.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Administrator" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = "Manager" });

            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Item>().HasData(
                    new Item { Id = i, Name = $"Item{i}", AvailableQuantity = i });
            }
        }

        public static void CreateMappings(this ModelBuilder modelBuilder)
        {
            var mappings = GetMainMappings();

            foreach (var mapping in mappings)
            {
                mapping.Visit(modelBuilder);
            }
        }

        private static IEnumerable<IMap> GetMainMappings()
        {
            var assemblyTypes = typeof(ItemMap).GetTypeInfo().Assembly.DefinedTypes;
            var mappings = assemblyTypes
                .Where(t => t.Namespace != null && t.Namespace.Contains(typeof(ItemMap).Namespace))
                .Where(t => typeof(IMap).GetTypeInfo().IsAssignableFrom(t));
            mappings = mappings.Where(x => !x.IsAbstract);
            return mappings.Select(m => (IMap)Activator.CreateInstance(m.AsType())).ToArray();
        }
    }
}
