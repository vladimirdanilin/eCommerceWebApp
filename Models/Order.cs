using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateOnly OrderDate { get; set; }

        public string ShippingAddress { get; set; }

        //Relationships

        public List<Order_Product> Order_Products { get; set; }

        //User

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }
    }
}
