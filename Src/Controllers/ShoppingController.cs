using Microsoft.AspNetCore.Mvc;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapperService _mapperService;
        public ShoppingController(IShoppingCartService shoppingCartService, IMapperService mapperService)
        {
            _shoppingCartService = shoppingCartService;
            _mapperService = mapperService;
        }

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