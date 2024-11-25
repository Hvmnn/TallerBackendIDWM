namespace TallerBackendIDWM.Src.Models
{

    public class Product 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required string Image { get; set; }
        
    }

}