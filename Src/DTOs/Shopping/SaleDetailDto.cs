namespace TallerBackendIDWM.Src.DTOs.Shopping
{
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public List<SaleItemDto> SaleItems { get; set; } = new();
    }
}