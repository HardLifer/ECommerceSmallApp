using ECommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSimpleAPI.DependencyInjection
{
    public static class DatabaseInejction
    {
        public static IServiceCollection AddProductContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnectionString");

            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            services.AddDbContextFactory<ProductContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}