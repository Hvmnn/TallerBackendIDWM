using TallerBackendIDWM.Src.DTOs.Shopping;

namespace TallerBackendIDWM.Src.Services.Interface
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetSalesAsync();
        Task<SaleDetailDto?> GetSaleByIdAsync(int id);
    }
}