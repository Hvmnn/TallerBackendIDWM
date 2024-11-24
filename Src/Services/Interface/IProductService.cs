using TallerBackendIDWM.Src.DTOs.Product;

namespace TallerBackendIDWM.Src.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(int id, CreateProductDto productDto);
        Task DeleteProductAsync(int id);
    }
}