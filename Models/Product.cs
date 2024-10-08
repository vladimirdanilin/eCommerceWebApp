﻿using ECommerceWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string PictureURL { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int TotalNumberOfStars { get; set; }
        public int TotalNumberOfRates { get; set; }

        public int Quantity { get; set; }

        public ProductCategory ProductCategory { get; set; }

        //Relationships
        public ICollection<Rating>? Ratings { get; set; }

        public ICollection<OrderProduct>? OrdersProducts { get; set; }

        public bool AvailableForSale { get; set; } = true;
    }
}
