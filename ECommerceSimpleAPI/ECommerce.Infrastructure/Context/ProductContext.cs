using ECommerce.Core.Models;
using ECommerce.Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {            
        }

        public ProductContext(DbContextOptions options)
            :base(options)
        {            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(ProductSeeder.Seed(countOfProdutsToGenerate: 50));
            // model configuration
        }
    }
}