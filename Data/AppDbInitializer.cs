using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            await SeedRolesAsync(userManager, roleManager);
            await SeedGoodsAsync(context);
        }

        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.SuperAdmin))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Roles.SuperAdmin));

                string superAdminEmail = "volodyadanilin@gmail.com";
                string superAdminPassword = "Vova25";
                var superAdminNormalizedEmail = userManager.NormalizeEmail(superAdminEmail);

                User superAdmin = new User
                {
                    Email = superAdminEmail,
                    NormalizedEmail = superAdminNormalizedEmail,
                    UserName = superAdminEmail,
                    FirstName = "Vladimir",
                    LastName = "Danilin"
                };

                var result = await userManager.CreateAsync(superAdmin, superAdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdmin, Roles.SuperAdmin);
                }
            }


            string[] roles = { Roles.SalesManager, Roles.WarehouseManager, Roles.Customer };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }

        public static async Task SeedGoodsAsync(AppDbContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(new List<Product>()
                {
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Leather Wallet",
                        Description = "Classic leather wallet with card slots and cash compartment.",
                        ProductCategory = ProductCategory.Accessories,
                        Price = 8.35,
                        Quantity = 5,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Women's Handbag",
                        Description = "Elegant handbag for women with spacious interior.",
                        ProductCategory = ProductCategory.Accessories,
                        Price = 11.25,
                        Quantity = 3,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Wireless Headphones",
                        Description = "Premium wireless headphones with noise-canceling technology.",
                        ProductCategory = ProductCategory.Electronics,
                        Price = 78.59,
                        Quantity = 0,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Smartphone Case",
                        Description = "Protective case for smartphones with sleek design.",
                        ProductCategory = ProductCategory.Accessories,
                        Price = 5.55,
                        Quantity = 1,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Running Shoes",
                        Description = "Comfortable running shoes with breathable material.",
                        ProductCategory = ProductCategory.Clothing,
                        Price = 12.02,
                        Quantity = 19,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Stainless Steel Watch",
                        Description = "Durable stainless steel watch with analog display.",
                        ProductCategory = ProductCategory.Accessories,
                        Price = 9.95,
                        Quantity = 10,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Travel Backpack",
                        Description = "Versatile backpack for travel and outdoor activities.",
                        ProductCategory = ProductCategory.Accessories,
                        Price = 9.95,
                        Quantity = 27,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Fitness Tracker",
                        Description = "Smart fitness tracker with heart rate monitoring.",
                        ProductCategory = ProductCategory.Electronics,
                        Price = 49.99,
                        Quantity = 23,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Coffee Maker",
                        Description = "Compact coffee maker for brewing fresh coffee at home.",
                        ProductCategory = ProductCategory.Electronics,
                        Price = 199.01,
                        Quantity = 10,
                    },
                    new Product()
                    {
                        PictureURL = "https://via.placeholder.com/500",
                        Name = "Portable Charger",
                        Description = "Pocket-sized portable charger for charging devices on the go.",
                        ProductCategory = ProductCategory.Electronics,
                        Price = 19.15,
                        Quantity = 70,
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}