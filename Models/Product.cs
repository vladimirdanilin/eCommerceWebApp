using eCommerceWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string PictureURL { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductCategory ProductCategory { get; set; }

        //Relationships

        public List<Order_Product> Orders_Products { get; set; }
    }
}
