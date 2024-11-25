namespace TallerBackendIDWM.Src.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
    }
}