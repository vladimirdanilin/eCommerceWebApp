using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double TotalUnitPrice { get; set; }

        //Relationships
        public int ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")]

        public ShoppingCart ShoppingCart { get; set; }
    }
}
