using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<IEnumerable<Sale>> GetSalesAsync();
    }
}