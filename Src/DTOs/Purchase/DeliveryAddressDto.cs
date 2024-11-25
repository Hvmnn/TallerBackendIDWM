namespace TallerBackendIDWM.Src.DTOs.Purchase
{
    public class DeliveryAddressDto
    {
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Commune { get; set; }
        public required string Street { get; set; }
    }

}