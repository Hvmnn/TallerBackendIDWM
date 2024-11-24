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
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(IProductRepository productRepository, IMapperService mapperService, ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _mapperService = mapperService;
            _cloudinaryService = cloudinaryService;
        }
        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(productDto.Image);
            var product = _mapperService.MapProduct(productDto);
            product.Image = imageUrl;

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

        public async Task UpdateProductAsync(int id, CreateProductDto productDto, IFormFile? imageFile)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            if(imageFile != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                product.Image = imageUrl;
            }

            product.Name = productDto.Name;
            product.Type = productDto.Type;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;

            await _productRepository.UpdateProductAsync(product);
        }
    }
}