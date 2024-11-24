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
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] CreateProductDto editproductDto, [FromForm] IFormFile? imageFile)
        {
            try
            {
                await _productService.UpdateProductAsync(id, editproductDto, imageFile);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }

}