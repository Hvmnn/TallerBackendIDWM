using Microsoft.AspNetCore.Mvc;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Controllers
{
    /// <summary>
    /// Controlador para gestionar el carrito de compras de los usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapperService _mapperService;

        /// <summary>
        /// Constructor del controlador del carrito de compras.
        /// </summary>
        /// <param name="shoppingCartService">Servicio de carrito de compras.</param>
        /// <param name="mapperService">Servicio de mapeo de datos.</param>
        public ShoppingController(IShoppingCartService shoppingCartService, IMapperService mapperService)
        {
            _shoppingCartService = shoppingCartService;
            _mapperService = mapperService;
        }

        /// <summary>
        /// Obtiene el carrito de compras de un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Carrito de compras del usuario.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cartDto = await _shoppingCartService.GetShoppingCartAsync(userId);

            if (cartDto == null)
            {
                return NotFound(new { message = "El carrito no existe." });
            }

            return Ok(cartDto);
        }

        /// <summary>
        /// Agrega un producto al carrito de compras de un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="cartItemDto">Detalles del producto a agregar.</param>
        /// <returns>Confirmación de la operación.</returns>
        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddItemToCart(int userId, [FromBody] CartItemDto cartItemDto)
        {
            if (cartItemDto.Quantity <= 0)
            {
                return BadRequest(new { message = "La cantidad debe ser mayor a cero." });
            }

            await _shoppingCartService.AddItemToCartAsync(userId, cartItemDto.ProductId, cartItemDto.Quantity);
            return Ok(new { message = "Producto agregado al carrito." });
        }

        /// <summary>
        /// Actualiza la cantidad de un producto en el carrito de un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="productId">ID del producto.</param>
        /// <param name="quantity">Nueva cantidad del producto.</param>
        [HttpPut("{userId}/update/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity(int userId, int productId, [FromBody] int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest(new { message = "La cantidad debe ser mayor a cero." });
            }

            try
            {
                await _shoppingCartService.UpdateItemQuantityAsync(userId, productId, quantity);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un producto del carrito de un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="productId">ID del producto.</param>
        [HttpDelete("{userId}/delete/{productId}")]
        public async Task<IActionResult> DeleteItemFromCart(int userId, int productId)
        {
            try
            {
                await _shoppingCartService.RemoveItemFromCartAsync(userId, productId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Limpia todos los productos del carrito de un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            try
            {
                await _shoppingCartService.ClearCartAsync(userId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}