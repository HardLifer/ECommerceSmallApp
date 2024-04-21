using ECommerce.Core.Models;
using ECommerce.Infrastructure.Context;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IDbContextFactory<ProductContext> _productContextFactory;

        public ProductService(ILogger<ProductService> logger, IDbContextFactory<ProductContext> productContextFactory)
        {
            _logger = logger;
            _productContextFactory = productContextFactory;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _logger.LogDebug("CreateProductAsync method started.");

            await using var dbContext = await _productContextFactory.CreateDbContextAsync();

            product.CreatedDate = DateTime.Now;


            var addedProduct = await dbContext.Products.AddAsync(product);

            await dbContext.SaveChangesAsync();

            _logger.LogDebug("CreateProductAsync method finished.");

            return addedProduct.Entity;
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            _logger.LogDebug("DeleteProductAsync method started.");

            await using var dbContext = await _productContextFactory.CreateDbContextAsync();

            var product = await dbContext.Products.FirstOrDefaultAsync(ent => ent.Id == productId);

            if (product == null)
            {
                _logger.LogError("The product with provided id={productId} doesn't exist. Provide a correct product ID.", productId);

                return false;
            }

            var deletedProduct = dbContext.Products.Remove(product);

            await dbContext.SaveChangesAsync();

            _logger.LogDebug("DeleteProductAsync method finished.");

            return deletedProduct != null ? true : false;
        }

        public async Task<Product> GetProductAsync(Guid productId)
        {
            _logger.LogDebug("GetProductAsync method started.");

            await using var dbContext = await _productContextFactory.CreateDbContextAsync();

            var product = await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(ent => ent.Id == productId);

            if (product == null)
            {
                _logger.LogError("The product with provided id={productId} doesn't exist. Provide a correct product ID.", productId);

                return null!;
            }

            _logger.LogDebug("GetProductAsync method finished.");

            return product!;
        }

        public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            _logger.LogDebug("GetProductsAsync method started.");

            await using var dbContext = await _productContextFactory.CreateDbContextAsync();

            var products = await dbContext.Products.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            _logger.LogDebug("GetProductsAsync method finished.");

            return products;
        }

        public async Task<Product> UpdateProductAsync(Guid productId, Product product)
        {
            _logger.LogDebug("UpdateProductAsync method started.");

            await using var dbContext = await _productContextFactory.CreateDbContextAsync();

            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(ent => ent.Id == productId);

            if (existingProduct == null)
            {
                _logger.LogError("The product with provided id={productId} doesn't exist. Provide a correct product ID.", productId);

                return null!;
            }

            // Can use AutoMapper here on MiniMapper
            // Provide default map

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.ModifiedDate = DateTime.UtcNow;

            dbContext.Products.Update(existingProduct);

            await dbContext.SaveChangesAsync();

            _logger.LogDebug("UpdateProductAsync method finished."); 
            
            return existingProduct;

            //Possible solution with .NET 7
            //await dbContext.Products.Where(ent => ent.Id == productId).ExecuteUpdateAsync(ent => 
            //    ent.SetProperty(n => n.Name, product.Name)
            //       .SetProperty(d => d.Description, product.Description)
            //       .SetProperty(p => p.Price, product.Price)
            //       .SetProperty(q => q.Quantity, product.Quantity)
            //       .SetProperty(md => md.ModifiedDate, DateTime.UtcNow)
        }
    }
}
