namespace TallerBackendIDWM.Src.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<PurchaseItem> Items { get; set; } = new();
    }


}