namespace TallerBackendIDWM.Src.DTOs.Shopping
{
    public class ShoppingCartDto
    {
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(item => item.Total);
    }
}