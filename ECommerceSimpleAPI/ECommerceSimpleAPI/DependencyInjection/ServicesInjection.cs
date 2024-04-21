using ECommerce.Services.Concrete;
using ECommerce.Services.Interfaces;

namespace ECommerceSimpleAPI.DependencyInjection
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddProductServices (this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}