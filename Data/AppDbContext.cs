using ECommerceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>().HasKey(op => new
            { 
                op.OrderId, 
                op.ProductId
            });

            modelBuilder.Entity<UserAddress>().HasKey(au => new
            {
                au.UserId,
                au.AddressId
            });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(o => o.Order)
                .WithMany(am => am.OrdersProducts)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(am => am.OrdersProducts)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<UserAddress>()
                .HasOne(u => u.User)
                .WithMany(am => am.UsersAddresses)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserAddress>()
                .HasOne(a => a.Address)
                .WithMany(am => am.UsersAddresses)
                .HasForeignKey(a => a.AddressId);

            base.OnModelCreating(modelBuilder);
        }

        //The definition of tables
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OrderProduct> OrdersProducts { get; set; }

        public DbSet<UserAddress> UsersAddresses { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }
}
