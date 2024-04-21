using ECommerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // model configuration
        }
    }
}
