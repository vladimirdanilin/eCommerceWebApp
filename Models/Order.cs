using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateOnly OrderDate { get; set; }

        //Relationships

        public List<Order_Product> Orders_Products { get; set; }

        //User

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }

        //Address

        public int ShippingAddressId { get; set; }
        [ForeignKey("ShippingAddressId")]

        public Address ShippingAddress { get; set; }    
    }
}
