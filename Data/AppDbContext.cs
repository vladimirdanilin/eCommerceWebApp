using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //For many to many
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order_Product>().HasKey(op => new
            { 
                op.OrderId, 
                op.ProductId
            });

            modelBuilder.Entity<Order_Product>().HasOne(o => o.Order).WithMany(am => am.Order_Products).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<Order_Product>().HasOne(p => p.Product).WithMany(am => am.Order_Products).HasForeignKey(p => p.ProductId);

            base.OnModelCreating(modelBuilder);
        }

        //The definition of tables
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order_Product> Orders_Products { get; set; }
    }
}
