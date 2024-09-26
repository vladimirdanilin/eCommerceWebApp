using ECommerceWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product picture is required")]
        public string PictureURL { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 chars")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Price is required")]
        public double Price { get; set; }

        public int TotalNumberOfStars { get; set; }
        public int TotalNumberOfRates { get; set; }

        public double RatingValue
        {
            get
            {
                if (TotalNumberOfRates == 0)
                {
                    return 0;
                }

                return (double)TotalNumberOfStars / TotalNumberOfRates;
            }
        }

        public ProductCategory ProductCategory { get; set; }

        public int Quantity { get; set; }

        public bool? AvailableForSale { get; set; }
    }
}