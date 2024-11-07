using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tallerBackendIDWM.Src.DTOs;
using tallerBackendIDWM.Src.Interfaces;
using tallerBackendIDWM.Src.Models;
using tallerBackendIDWM.Src.Services;

namespace tallerBackendIDWM.Src.Controllers{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICloudinaryService _cloudinaryServices;

        public ProductController(IProductRepository productRepository, ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _cloudinaryServices = cloudinaryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromForm] CreateProductDTO productDto)
        {
            var supportedFormats = new[] { ".jpg", ".png"};

            var extension = Path.GetExtension(productDto.Image.FileName).ToLower();

            if (!supportedFormats.Contains(extension))
            {
                return BadRequest("Formato de imagen no válido. Formatos soportados: .jpg, .png");
            }

            if (productDto.Image.Length > 10 * 1024 * 1024)
            {
                return BadRequest("El tamaño máximo del archivo es 10 MB");
            }

            string imagenUrl;
            try
            {
                imagenUrl = await _cloudinaryServices.UploadImageAsync(productDto.Image);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir la imagen: {ex.Message}");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Type = productDto.Type,
                Price = productDto.Price,
                Stock = productDto.Stock,
                Image = imagenUrl
            };

            await _productRepository.CreateProductAsync(product);
            
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] CreateProductDTO editproductDto)
        {
            var productToUpdate = await _productRepository.GetProductById(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = editproductDto.Name;
            productToUpdate.Type = editproductDto.Type;
            productToUpdate.Price = editproductDto.Price;
            productToUpdate.Stock = editproductDto.Stock;

            if (editproductDto.Image != null)
            {
                var supportedFormats = new[] { ".jpg", ".png" };
                var extension = Path.GetExtension(editproductDto.Image.FileName).ToLower();

                if (!supportedFormats.Contains(extension))
                {
                    return BadRequest("Formato de imagen no válido. Formatos soportados: .jpg, .png");
                }

                if (editproductDto.Image.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("El tamaño máximo del archivo es 10 MB");
                }

                string imagenUrl;
                try
                {
                    imagenUrl = await _cloudinaryServices.UploadImageAsync(editproductDto.Image);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al subir la imagen: {ex.Message}");
                }

                productToUpdate.Image = imagenUrl;
            }
            

            await _productRepository.UpdateProductAsync(productToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
    }

}