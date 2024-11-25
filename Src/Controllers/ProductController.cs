using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TallerBackendIDWM.Src.DTOs.Product;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services;
using TallerBackendIDWM.Src.Services.Implements;
using TallerBackendIDWM.Src.Services.Interface;

namespace tallerBackendIDWM.Src.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Producto no encontrado." });
            }

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto productDto)
        {
            if(productDto.Image == null || productDto.Image.Length == 0)
            {
                return BadRequest(new {message = "La imagen es requerida"});
            }

            await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Name }, productDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto editproductDto)
        {
            try
            {
                var productDto = new CreateProductDto
                {
                    Name = editproductDto.Name,
                    Type = editproductDto.Type,
                    Price = editproductDto.Price,
                    Stock = editproductDto.Stock,
                    Image = null
                };
                await _productService.UpdateProductAsync(id, productDto, editproductDto.Image);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] string? type, [FromQuery] string? orderBy)
        {
            var products = await _productService.GetProductsAsync();

            if (!string.IsNullOrEmpty(type))
            {
                products = products.Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (orderBy == "asc")
            {
                products = products.OrderBy(p => p.Price).ToList();
            }
            else if (orderBy == "desc")
            {
                products = products.OrderByDescending(p => p.Price).ToList();
            }

            return Ok(products);
        }

    }

}