using ECommerceSimpleAPI.DTOs;
using ECommerceSimpleAPI.Models;
using ECommerceSimpleAPI.Validators;
using FluentValidation;

namespace ECommerceSimpleAPI.DependencyInjection
{
    public static class ValidatorsInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PaginationSettings>, PaginationValidator>();
            services.AddScoped<IValidator<ProductDTO>, ProductDTOValidator>();

            return services;
        }
    }
}
