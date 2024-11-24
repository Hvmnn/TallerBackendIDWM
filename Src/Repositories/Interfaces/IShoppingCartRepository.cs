namespace TallerBackendIDWM.Src.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TallerBackendIDWM.Src.Models;

    public interface IShoppingCartRepository
    {
        Task CreateAsync(ShoppingCart cart);
        Task UpdateAsync(ShoppingCart cart);
        Task<ShoppingCart?> GetShoppingCart(int userId);
        Task AddCartItem(int cartId, CartItem cartItem);
        Task UpdateCartItem(int cartId, int productId, int quantity);
        Task DeleteCartItem(int cartId, int productId);
        Task ClearShoppingCart(int cartId);
    }
}