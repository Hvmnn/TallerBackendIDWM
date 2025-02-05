namespace TallerBackendIDWM.Src.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; } = null!;
    }
}