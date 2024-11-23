using Microsoft.AspNetCore.Http.Features;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.Helpers;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapperService _mapperService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapperService mapperService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapperService = mapperService;
        }
        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            }

            var cart = await _shoppingCartRepository.GetShoppingCart(userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, CartItems = new List<CartItem>() };
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }

            await _shoppingCartRepository.AddCartItem(cart.Id, new CartItem { ProductId = productId, Quantity = quantity });
        }

        public async Task UpdateItemQuantityAsync(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            }

            var cart = await _shoppingCartRepository.GetShoppingCart(userId);

            if (cart == null)
            {
                throw new ArgumentException("El carrito no existe.");
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem == null)
            {
                throw new ArgumentException("El producto no existe en el carrito.");
            }

            existingItem.Quantity = quantity;

            await _shoppingCartRepository.UpdateCartItem(cart.Id, productId, quantity);
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCart(userId);

            if (cart == null)
            {
                throw new ArgumentException("El carrito no existe.");
            }

            await _shoppingCartRepository.ClearShoppingCart(cart.Id);
        }

        public async Task<ShoppingCartDto> GetShoppingCartAsync(int userId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCart(userId);

            if (cart == null) return null;

            return _mapperService.MapShoppingCart(cart);
        }

        public async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCart(userId);

            if(cart == null)
            {
                throw new ArgumentException("El carrito no existe.");
            }

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if(item == null)
            {
                throw new ArgumentException("El producto no existe en el carrito.");
            }

            await _shoppingCartRepository.DeleteCartItem(cart.Id, productId);
        }
    }
}