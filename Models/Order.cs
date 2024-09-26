using ECommerceWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateOnly OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public double Total { get; set; }

        //Relationships

        public ICollection<OrderProduct>? OrdersProducts { get; set; }

        //User

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }

        //Address

        public int? ShippingAddressId { get; set; }
        [ForeignKey("ShippingAddressId")]

        public Address? ShippingAddress { get; set; }
    }
}
