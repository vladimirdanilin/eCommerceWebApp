using eCommerceWebApp.Data.Enums;
using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
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
                            ProfilePictureURL = "https://via.placeholder.com/300"
                        },
                        new User()
                        {
                            FullName = "Alice Smith",
                            PhoneNumber = "9876543210",
                            Email = "AliceSmith@gmail.com",
                            Password = "54321",
                            ProfilePictureURL = "https://via.placeholder.com/300"
                        },
                        new User()
                        {
                            FullName = "Emma Thompson",
                            PhoneNumber = "5551234567",
                            Email = "EmmaThompson@gmail.com",
                            Password = "emmathompson",
                            ProfilePictureURL = "https://via.placeholder.com/300"
                        },
                        new User()
                        {
                            FullName = "Michael Johnson",
                            PhoneNumber = "738428632",
                            Email = "MichaelJohnson@gmail.com",
                            Password = "michaeljohnson",
                            ProfilePictureURL = "https://via.placeholder.com/300"
                        }
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
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Leather Wallet",
                            Description = "Classic leather wallet with card slots and cash compartment.",
                            ProductCategory = ProductCategory.Accessories,
                            Price = 8.35,
                            Quantity = 5,
                            Rating = 4.5
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Women's Handbag",
                            Description = "Elegant handbag for women with spacious interior.",
                            ProductCategory = ProductCategory.Accessories,
                            Price = 11.25,
                            Quantity = 3,
                            Rating = 4.3
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Wireless Headphones",
                            Description = "Premium wireless headphones with noise-canceling technology.",
                            ProductCategory = ProductCategory.Electronics,
                            Price = 78.59,
                            Quantity = 0,
                            Rating = 4.6
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Smartphone Case",
                            Description = "Protective case for smartphones with sleek design.",
                            ProductCategory = ProductCategory.Accessories,
                            Price = 5.55,
                            Quantity = 1,
                            Rating = 4.2
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Running Shoes",
                            Description = "Comfortable running shoes with breathable material.",
                            ProductCategory = ProductCategory.Clothing,
                            Price = 12.02,
                            Quantity = 19,
                            Rating = 4.1
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Stainless Steel Watch",
                            Description = "Durable stainless steel watch with analog display.",
                            ProductCategory = ProductCategory.Accessories,
                            Price = 9.95,
                            Quantity = 10,
                            Rating = 4.9
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Travel Backpack",
                            Description = "Versatile backpack for travel and outdoor activities.",
                            ProductCategory = ProductCategory.Accessories,
                            Price = 9.95,
                            Quantity = 27,
                            Rating = 4.6
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Fitness Tracker",
                            Description = "Smart fitness tracker with heart rate monitoring.",
                            ProductCategory = ProductCategory.Electronics,
                            Price = 49.99,
                            Quantity = 23,
                            Rating = 4.7
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Coffee Maker",
                            Description = "Compact coffee maker for brewing fresh coffee at home.",
                            ProductCategory = ProductCategory.Electronics,
                            Price = 199.01,
                            Quantity = 10,
                            Rating = 4.3
                        },
                        new Product()
                        {
                            PictureURL = "https://via.placeholder.com/500",
                            Name = "Portable Charger",
                            Description = "Pocket-sized portable charger for charging devices on the go.",
                            ProductCategory = ProductCategory.Electronics,
                            Price = 19.15,
                            Quantity = 70,
                            Rating = 4.0
                        }
                    });
                    context.SaveChanges();
                }

                //Addresses
                if (!context.Addresses.Any())
                {
                    context.Addresses.AddRange(new List<Address>()
                    {
                        new Address
                        {
                            Street = "123 Main St",
                            City = "New York",
                            PostalOrZipCode = "10001",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "456 Elm St",
                            City = "Los Angeles",
                            PostalOrZipCode = "90001",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "789 Oak St",
                            City = "Chicago",
                            PostalOrZipCode = "60601",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "101 Pine St",
                            City = "San Francisco",
                            PostalOrZipCode = "94101",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "222 Maple St",
                            City = "Seattle",
                            PostalOrZipCode = "98101",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "333 Cedar St",
                            City = "Boston",
                            PostalOrZipCode = "02101",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "444 Birch St",
                            City = "Miami",
                            PostalOrZipCode = "33101",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "555 Walnut St",
                            City = "Dallas",
                            PostalOrZipCode = "75201",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "666 Elm St",
                            City = "Houston",
                            PostalOrZipCode = "77001",
                            Country = "USA"
                        },
                        new Address
                        {
                            Street = "777 Pine St",
                            City = "Atlanta",
                            PostalOrZipCode = "30301",
                            Country = "USA"
                        },
                    });
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
                            ShippingAddressId = 5,
                        },
                        new Order()
                        {
                            OrderDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                            UserId = 4,
                            ShippingAddressId = 7
                        },
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
                

                if (!context.Users_Addresses.Any())
                {
                    context.Users_Addresses.AddRange(new List<User_Address>()
                    { 
                        new User_Address
                        {
                            UserId = 1,
                            AddressId = 1
                        },
                        new User_Address
                        {
                            UserId = 1,
                            AddressId = 3
                        },
                        new User_Address
                        {
                            UserId = 2,
                            AddressId = 2
                        },
                        new User_Address
                        {
                            UserId = 3,
                            AddressId = 4
                        },
                        new User_Address
                        {
                            UserId = 3,
                            AddressId = 5
                        },
                        new User_Address
                        {
                            UserId = 4,
                            AddressId = 6
                        },
                        new User_Address
                        {
                            UserId = 4,
                            AddressId = 7
                        },
                        new User_Address
                        {
                            UserId = 4,
                            AddressId = 8
                        },
                        new User_Address
                        {
                            UserId = 4,
                            AddressId = 9
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
