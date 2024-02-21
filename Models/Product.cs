using eCommerceWebApp.Data.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Picture Url")]
        [Required(ErrorMessage = "Product picture is required")]
        public string PictureURL { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 chars")]
        public string Name { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Product Description is required")]
        public string Description { get; set; }

        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product Rating")]
        public double Rating { get; set; }

        [Display(Name = "Product Quantity In Stock")]
        public double Quantity { get; set; }

        public ProductCategory ProductCategory { get; set; }

        //Relationships

        public List<Order_Product>? Orders_Products { get; set; }
    }
}
