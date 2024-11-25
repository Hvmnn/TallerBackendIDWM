namespace TallerBackendIDWM.Src.DTOs.Purchase
{
    public class ConfirmPurchaseDto
    {
        public required int DeliveryAddressId { get; set; }
        public required string PaymentMethod { get; set; }
    }

}