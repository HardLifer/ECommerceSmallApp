using ECommerce.Core.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(Guid productId);

        Task<Product> CreateProductAsync(Product product);

        Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize);

        Task<Product> UpdateProductAsync(Guid productId, Product product);

        Task<bool> DeleteProductAsync(Guid productId);
    }
}
