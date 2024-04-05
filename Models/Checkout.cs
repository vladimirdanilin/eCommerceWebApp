using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }



        public List<Address> userAddresses { get; set; }

        //Relationships
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
