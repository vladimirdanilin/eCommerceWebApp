using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }

        public List<Address> UserAddresses { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
