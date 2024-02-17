using eCommerceWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Picture Url")]
        public string PictureURL { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Description")]
        public string Description { get; set; }

        public ProductCategory ProductCategory { get; set; }

        //Relationships

        public List<Order_Product> Orders_Products { get; set; }
    }
}
