using Microsoft.EntityFrameworkCore;
using tallerBackendIDWM.Src.Models;

namespace tallerBackendIDWM.Src.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}