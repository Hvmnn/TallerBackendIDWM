namespace tallerBackendIDWM.Src.DTOs{
    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required int Price { get; set; }
        public required int Stock { get; set; }
        public required IFormFile Image { get; set; }
    }
}