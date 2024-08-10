using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        //Relationships

        public List<UserAddress> UsersAddresses { get; set; }
        
    }
}
