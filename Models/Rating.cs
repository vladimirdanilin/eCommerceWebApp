using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebApp.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Range(1,5)]
        public int NumberOfStars { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }
    }
}
