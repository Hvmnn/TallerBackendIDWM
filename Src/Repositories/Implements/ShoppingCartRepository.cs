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
            _context.ShoppingCarts.Update(cart);
            await _context.SaveChangesAsync();
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