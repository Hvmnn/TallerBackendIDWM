namespace TallerBackendIDWM.Src.DTOs.Shopping
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => ProductPrice * Quantity;
    }
}