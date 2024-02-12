using System.ComponentModel.DataAnnotations;

namespace eCommerceWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserID { get; set; }

        public DateOnly OrderDate { get; set; }

        public string ShippingAddress { get; set; }

        //Relationships

        public List<Order_Product> Order_Products { get; set; }
    }
}
