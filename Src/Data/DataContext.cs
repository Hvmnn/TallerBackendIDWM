using Microsoft.EntityFrameworkCore;
using TallerBackendIDWM.Src.Models;

namespace TallerBackendIDWM.Src.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Gender> Genders {get; set;} = null!;
        public DbSet<Role> Roles {get; set;} = null!;
        public DbSet<User> Users {get; set;} = null!;
    }
}