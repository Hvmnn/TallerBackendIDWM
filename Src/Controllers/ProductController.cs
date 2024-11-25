using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TallerBackendIDWM.Src.DTOs.Product;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services;
using TallerBackendIDWM.Src.Services.Implements;
using TallerBackendIDWM.Src.Services.Interface;

namespace tallerBackendIDWM.Src.Controllers
{
    /// <summary>
    /// Controlador para gestionar productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor del controlador de productos.
        /// </summary>
        /// <param name="productService">Servicio de productos.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>Producto correspondiente al ID.</returns>
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

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productDto">Datos del producto a crear.</param>
        /// <returns>Producto creado.</returns>
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

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a actualizar.</param>
        /// <param name="editProductDto">Datos del producto actualizados.</param>
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

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Filtra y ordena los productos según criterios específicos.
        /// </summary>
        /// <param name="type">Tipo de producto.</param>
        /// <param name="orderBy">Orden de los precios (asc o desc).</param>
        /// <param name="searchString">Término de búsqueda por nombre.</param>
        /// <returns>Lista de productos filtrados y ordenados.</returns>
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] string? type, [FromQuery] string? orderBy, [FromQuery] string? searchString)
        {
            var products = await _productService.GetProductsAsync();

            if (!string.IsNullOrEmpty(type))
            {
                products = products.Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
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