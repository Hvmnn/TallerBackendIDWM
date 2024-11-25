namespace TallerBackendIDWM.Src.DTOs.Shopping
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
    }
}