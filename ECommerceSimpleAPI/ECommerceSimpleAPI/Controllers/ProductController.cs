using ECommerce.Core.Models;
using ECommerce.Services.Interfaces;
using ECommerceSimpleAPI.DTOs;
using ECommerceSimpleAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceSimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        private readonly IValidator<PaginationSettings> _paginationValidator;
        private readonly IValidator<ProductDTO> _productValidator;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IValidator<PaginationSettings> paginationValidator, IValidator<ProductDTO> productValidator)
        {
            _logger = logger;
            _productService = productService;
            _paginationValidator = paginationValidator;
            _productValidator = productValidator;
        }

        [HttpGet("getproductbyid")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductById([FromQuery] Guid productId)
        {
            try
            {
                var result = await _productService.GetProductAsync(productId);

                if (result == null)
                {
                    _logger.LogError($"Product with provided id={productId} doesn't exist.");

                    return NotFound($"Product with provided id={productId} doesn't exist.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getproducts")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProducts([FromQuery]PaginationSettings paginationSettings)
        {
            try
            {
                var validationResults = await _paginationValidator.ValidateAsync(paginationSettings);

                if (!validationResults.IsValid)
                {                    
                    return BadRequest(validationResults.ToString());
                }

                var result = await _productService.GetProductsAsync(paginationSettings.PageNumber, paginationSettings.PageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createproduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            try
            {
                var validationResults = await _productValidator.ValidateAsync(productDto);

                if (!validationResults.IsValid)
                {
                    return BadRequest(validationResults.ToString());
                }

                var product = new Product
                {
                    Description = productDto.Description,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                var result = await _productService.CreateProductAsync(product);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateproduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromQuery] Guid productId, ProductDTO productDto)
        {
            try
            {
                var validationResults = await _productValidator.ValidateAsync(productDto);

                if (!validationResults.IsValid)
                {
                    return BadRequest(validationResults.ToString());
                }

                var product = new Product
                {
                    Description = productDto.Description,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                };

                var result = await _productService.UpdateProductAsync(productId, product);

                if (result == null)
                {
                    _logger.LogError($"Product with provided id={productId} doesn't exist.");

                    return NotFound($"Product with provided id={productId} doesn't exist.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteproduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid productId)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(productId);

                if (!result)
                {
                    return NotFound(HttpStatusCode.NotFound.ToString());
                }

                return Ok(HttpStatusCode.OK.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
