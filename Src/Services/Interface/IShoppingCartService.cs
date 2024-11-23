using TallerBackendIDWM.Src.DTOs.Shopping;

namespace TallerBackendIDWM.Src.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCartAsync(int userId);
        Task AddItemToCartAsync(int userId, int productId, int quantity);
        Task UpdateItemQuantityAsync(int userId, int productId, int quantity);
        Task RemoveItemFromCartAsync(int userId, int productId);
        Task ClearCartAsync(int userId);
    }
}