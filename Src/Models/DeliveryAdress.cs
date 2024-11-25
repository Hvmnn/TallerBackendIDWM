namespace TallerBackendIDWM.Src.Models
{
    public class DeliveryAddress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Commune { get; set; } = null!;
    public string Street { get; set; } = null!;
}

}