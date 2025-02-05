using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TallerBackendIDWM.Src.Data;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapperService _mapperService;
        private readonly DataContext _context;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapperService mapperService, DataContext context)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapperService = mapperService;
            _context = context;
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
                await _shoppingCartRepository.CreateAsync(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = null! // No incluyas el objeto completo del producto
                };

                cart.CartItems.Add(newItem);
            }

            await _shoppingCartRepository.UpdateAsync(cart);
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

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, CartItems = new List<CartItem>() };

                await _shoppingCartRepository.CreateAsync(cart);
            }

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