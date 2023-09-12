using Microsoft.EntityFrameworkCore;

namespace ShopApp.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(x => new { x.OrderId, x.ProductId });
            });
        }


        public DbSet<Ads> ads { set; get; }
        public DbSet<Category> categories { set; get; }
        public DbSet<Contact> contacts { set; get; }
        public DbSet<New> news { set; get; }
        public DbSet<OrderDetails> orderDetails { set; get; }
        public DbSet<Orders> orders { set; get; }
        public DbSet<Post> posts { set; get; }
        public DbSet<ProductCategory> productCategories { set; get; }
        public DbSet<ProductImage> productImages { set; get; }
        public DbSet<Products> products { set; get; }


    }


}
