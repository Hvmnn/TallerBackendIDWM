using Microsoft.EntityFrameworkCore;
using TallerBackendIDWM.Src.Data;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;

namespace TallerBackendIDWM.Src.Repositories.Implements
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ShoppingCart cart)
        {
            await _context.ShoppingCarts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShoppingCart cart)
        {
            // Obtén el carrito actual desde la base de datos, incluyendo los CartItems
            var existingCart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            if (existingCart != null)
            {
                // Actualiza las propiedades del carrito
                _context.Entry(existingCart).CurrentValues.SetValues(cart);

                // Procesa cada CartItem en el carrito
                foreach (var item in cart.CartItems)
                {
                    var existingItem = existingCart.CartItems
                        .FirstOrDefault(ci => ci.ProductId == item.ProductId);

                    if (existingItem != null)
                    {
                        // Actualiza el CartItem existente
                        existingItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        // Agrega un nuevo CartItem
                        existingCart.CartItems.Add(item);
                    }
                }

                // Elimina CartItems que ya no están en el carrito actualizado
                foreach (var existingItem in existingCart.CartItems.ToList())
                {
                    if (!cart.CartItems.Any(ci => ci.ProductId == existingItem.ProductId))
                    {
                        _context.CartItems.Remove(existingItem);
                    }
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El carrito no existe.");
            }
        }


        public async Task<ShoppingCart?> GetShoppingCart(int userId)
            => await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

        public async Task AddCartItem(int cartId, CartItem cartItem)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.Id == cartId);
            
            if (shoppingCart != null)
            {
                shoppingCart.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItem(int cartId, int productId, int quantity)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.Id == cartId);

            var item = shoppingCart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCartItem(int cartId, int productId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.Id == cartId);

            var item = shoppingCart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (item != null)
            {
                shoppingCart?.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearShoppingCart(int cartId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.Id == cartId);

            if (shoppingCart != null)
            {
                shoppingCart.CartItems.Clear();
                await _context.SaveChangesAsync();
            }
        }
    }
}