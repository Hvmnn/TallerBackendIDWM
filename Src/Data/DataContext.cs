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
        public DbSet<CartItem> CartItems {get; set;} = null!;
        public DbSet<ShoppingCart> ShoppingCarts {get; set;} = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(c => c.CartItems)
                .WithOne()
                .HasForeignKey(c => c.Id);
        }
    }
}