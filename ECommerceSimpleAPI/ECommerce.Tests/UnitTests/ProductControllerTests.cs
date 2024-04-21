using ECommerce.Core.Models;
using ECommerce.Infrastructure.Seeder;
using ECommerce.Services.Interfaces;
using ECommerceSimpleAPI.Controllers;
using ECommerceSimpleAPI.DTOs;
using ECommerceSimpleAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ECommerce.Tests.UnitTests
{
    public class ProductControllerTests

    {
        [Fact]
        public async Task ProductController_GetProductById_ReturnExistingEntityByItsId()
        {
            var dataSet = ProductSeeder.Seed(2);
            var product = dataSet.First();
            var productId = product.Id;

            var productServicetMock = new Mock<IProductService>();
            productServicetMock.Setup(x => x.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);

            var loggerContext = new Mock<ILogger<ProductController>>();

            var paginationValidator = new Mock<IValidator<PaginationSettings>>();
            var productDtoValidator = new Mock<IValidator<ProductDTO>>();

            ProductController productController = new ProductController(loggerContext.Object, productServicetMock.Object, paginationValidator.Object, productDtoValidator.Object);
            var result = await productController.GetProductById(productId);
            var okResult = ((OkObjectResult)result).Value as Product;

            Assert.NotNull(okResult);
            Assert.Equal(okResult.Id, productId);
        }
    }
}