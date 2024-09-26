using ECommerceWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string PictureURL { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int TotalNumberOfStars { get; set; }
        public int TotalNumberOfRates { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public int Quantity { get; set; }

        public bool? AvailableForSale { get; set; }
    }   
}
