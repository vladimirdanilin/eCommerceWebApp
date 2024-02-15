using eCommerceWebApp.Data.Enums;
using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data
{
    public class AppDbInitializer
    {
        /*public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Users
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            FullName = "John Snow",
                            PhoneNumber = "1234567890",
                            Email = "JohnSnow@gmail.com",
                            Password = "12345",
                            ProfilePictureURL = ""
                        },
                        new User()
                        {
                            FullName = "Alice Smith",
                            PhoneNumber = "9876543210",
                            Email = "AliceSmith@gmail.com",
                            Password = "54321",
                            ProfilePictureURL = ""
                        },
                        new User()
                        {
                            FullName = "Emma Thompson",
                            PhoneNumber = "5551234567",
                            Email = "EmmaThompson@gmail.com",
                            Password = "emmathompson",
                            ProfilePictureURL = ""
                        },
                        new User()
                        {
                            FullName = "Michael Johnson",
                            PhoneNumber = "738428632",
                            Email = "MichaelJohnson@gmail.com",
                            Password = "michaeljohnson",
                            ProfilePictureURL = ""
                        }
                    });

                    var users = context.Users.ToList();

                    foreach (var user in users)
                    { 
                    user.ShippingAddresses.AddRange(context.Addresses.Where(a => a.UserId == user.Id).ToList());
                    }
                    context.SaveChanges();
                }
                //Orders
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(new List<Order>()
                    {
                        new Order() 
                        {
                            OrderDate = DateOnly.FromDateTime(DateTime.Now),
                            UserId = 1,
                            ShippingAddressId = 1
                        },
                        new Order()
                        {
                            OrderDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), 
                            UserId = 2,
                            ShippingAddressId = 2
                        },

                        new Order()
                        {
                            OrderDate = DateOnly.FromDateTime(DateTime.Now),
                            UserId = 3,
                            ShippingAddressId = 2,
                        },
                        new Order()
                        {
                            OrderDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), 
                            UserId = 4,
                            ShippingAddressId = 1
                        },
                    });
                    context.SaveChanges();
                }
                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            PictureURL = "https://example.com/product1.jpg",
                            Name = "Leather Wallet",
                            Description = "Classic leather wallet with card slots and cash compartment.",
                            ProductCategory = ProductCategory.Accessories
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product2.jpg",
                            Name = "Women's Handbag",
                            Description = "Elegant handbag for women with spacious interior.",
                            ProductCategory = ProductCategory.Accessories
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product3.jpg",
                            Name = "Wireless Headphones",
                            Description = "Premium wireless headphones with noise-canceling technology.",
                            ProductCategory = ProductCategory.Electronics
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product4.jpg",
                            Name = "Smartphone Case",
                            Description = "Protective case for smartphones with sleek design.",
                            ProductCategory = ProductCategory.Accessories
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product5.jpg",
                            Name = "Running Shoes",
                            Description = "Comfortable running shoes with breathable material.",
                            ProductCategory = ProductCategory.Clothing
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product6.jpg",
                            Name = "Stainless Steel Watch",
                            Description = "Durable stainless steel watch with analog display.",
                            ProductCategory = ProductCategory.Accessories
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product7.jpg",
                            Name = "Travel Backpack",
                            Description = "Versatile backpack for travel and outdoor activities.",
                            ProductCategory = ProductCategory.Accessories
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product8.jpg",
                            Name = "Fitness Tracker",
                            Description = "Smart fitness tracker with heart rate monitoring.",
                            ProductCategory = ProductCategory.Electronics
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product9.jpg",
                            Name = "Coffee Maker",
                            Description = "Compact coffee maker for brewing fresh coffee at home.",
                            ProductCategory = ProductCategory.Electronics
                        },
                        new Product()
                        {
                            PictureURL = "https://example.com/product10.jpg",
                            Name = "Portable Charger",
                            Description = "Pocket-sized portable charger for charging devices on the go.",
                            ProductCategory = ProductCategory.Electronics
                        }
                    });
                    context.SaveChanges();
                }
                //Orders and Products
                if (!context.Orders_Products.Any())
                {
                    context.Orders_Products.AddRange(new List<Order_Product>()
                    { 
                        new Order_Product()
                        {
                            OrderId = 1,
                            ProductId = 1
                        },
                        new Order_Product()
                        {
                            OrderId = 1,
                            ProductId = 3
                        },
                        new Order_Product()
                        {
                            OrderId = 2,
                            ProductId = 7
                        },
                        new Order_Product()
                        {
                            OrderId = 2,
                            ProductId = 8
                        },
                        new Order_Product()
                        {
                            OrderId = 2,
                            ProductId = 9
                        },
                        new Order_Product()
                        {
                            OrderId = 3,
                            ProductId = 6

                        },
                        new Order_Product()
                        {
                            OrderId = 3,
                            ProductId = 1
                        },
                        new Order_Product()
                        {
                            OrderId = 4,
                            ProductId = 7
                        },
                        new Order_Product()
                        {
                            OrderId = 4,
                            ProductId = 4
                        }
                    });
                    context.SaveChanges();
                }
                //Addresses
                if (context.Addresses.Any())
                {
                    context.Addresses.AddRange(new List<Address>()
                    {
                        new Address
                        {
                            Street = "123 Main St",
                            City = "New York",
                            PostalOrZipCode = "10001",
                            Country = "USA",
                            UserId = 1
                        },
                        new Address
                        {
                            Street = "456 Elm St",
                            City = "Los Angeles",
                            PostalOrZipCode = "90001",
                            Country = "USA",
                            UserId = 2
                        },
                        new Address
                        {
                            Street = "789 Oak St",
                            City = "Chicago",
                            PostalOrZipCode = "60601",
                            Country = "USA",
                            UserId = 1
                        },
                        new Address
                        {
                            Street = "101 Pine St",
                            City = "San Francisco",
                            PostalOrZipCode = "94101",
                            Country = "USA",
                            UserId = 4
                        },
                        new Address
                        {
                            Street = "222 Maple St",
                            City = "Seattle",
                            PostalOrZipCode = "98101",
                            Country = "USA",
                            UserId = 3
                        },
                        new Address
                        {
                            Street = "333 Cedar St",
                            City = "Boston",
                            PostalOrZipCode = "02101",
                            Country = "USA",
                            UserId = 2
                        },
                        new Address
                        {
                            Street = "444 Birch St",
                            City = "Miami",
                            PostalOrZipCode = "33101",
                            Country = "USA",
                            UserId = 4
                        },
                        new Address
                        {
                            Street = "555 Walnut St",
                            City = "Dallas",
                            PostalOrZipCode = "75201",
                            Country = "USA",
                            UserId = 1
                        },
                        new Address
                        {
                            Street = "666 Elm St",
                            City = "Houston",
                            PostalOrZipCode = "77001",
                            Country = "USA",
                            UserId = 3
                        },
                        new Address
                        {
                            Street = "777 Pine St",
                            City = "Atlanta",
                            PostalOrZipCode = "30301",
                            Country = "USA",
                            UserId = 2
                        },
                    });
                }
            }
        }*/
    }
}
