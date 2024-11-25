namespace TallerBackendIDWM.Src.DTOs.Shopping
{
    public class SaleItemDto
    {
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}