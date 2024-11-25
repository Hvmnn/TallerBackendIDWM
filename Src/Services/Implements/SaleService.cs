using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapperService _mapperService;

        public SaleService (ISaleRepository saleRepository, IMapperService mapperService)
        {
            _saleRepository = saleRepository;
            _mapperService = mapperService;
        }

        public async Task<SaleDetailDto?> GetSaleByIdAsync(int id)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(id);
            return sale == null ? null : _mapperService.MapSaleDetail(sale);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesAsync()
        {
            var sales = await _saleRepository.GetSalesAsync();
            return _mapperService.MapSales(sales);
        }
    }
}