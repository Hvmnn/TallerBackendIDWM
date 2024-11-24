using Bogus.DataSets;

namespace TallerBackendIDWM.Src.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new();
    }

}