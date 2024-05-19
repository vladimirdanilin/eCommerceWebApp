using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Data
{
    public class AppDbContext: DbContext
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

            modelBuilder.Entity<User_Address>().HasKey(au => new
            {
                au.UserId,
                au.AddressId
            });

            modelBuilder.Entity<Order_Product>().HasOne(o => o.Order).WithMany(am => am.Orders_Products).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<Order_Product>().HasOne(p => p.Product).WithMany(am => am.Orders_Products).HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<User_Address>().HasOne(u => u.User).WithMany(am => am.Users_Addresses).HasForeignKey(u => u.UserId);
            modelBuilder.Entity<User_Address>().HasOne(a => a.Address).WithMany(am => am.Users_Addresses).HasForeignKey(a => a.AddressId);

            base.OnModelCreating(modelBuilder);
        }

        //The definition of tables
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order_Product> Orders_Products { get; set; }

        public DbSet<User_Address> Users_Addresses { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Checkout> Checkouts { get; set; }
    }
}
