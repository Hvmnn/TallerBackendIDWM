using TallerBackendIDWM.Src.DTOs;
using TallerBackendIDWM.Src.DTOs.Product;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapperService _mapperService;

        public ProductService(IProductRepository productRepository, IMapperService mapperService)
        {
            _productRepository = productRepository;
            _mapperService = mapperService;
        }
        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapperService.MapProduct(productDto);
            await _productRepository.CreateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return product == null ? null : _mapperService.MapProduct(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return _mapperService.MapProducts(products);
        }

        public async Task UpdateProductAsync(int id, CreateProductDto productDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            product = _mapperService.MapProduct(productDto);
            product.Id = id;
            await _productRepository.UpdateProductAsync(product);
        }
    }
}