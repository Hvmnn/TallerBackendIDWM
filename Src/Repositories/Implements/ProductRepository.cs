using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;

namespace TallerBackendIDWM.Src.Repositories{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product with id {id} not found.");
            }
            return product;
        }

        public async Task CreateProductAsync(Product product)
        {
            bool productExists = await _context.Products.
            AnyAsync(p => p.Name == product.Name && p.Type == product.Type);

            if (productExists)
            {
                throw new InvalidOperationException("Ya existe un producto con el mismo nombre y tipo.");
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            bool productExists = await _context.Products.
            AnyAsync(p => p.Name == product.Name && p.Type == product.Type);

            if (productExists)
            {
                throw new InvalidOperationException("Ya existe un producto con el mismo nombre y tipo.");
            }
            
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        Task<IEnumerable<Product>> IProductRepository.GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductRepository.GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}