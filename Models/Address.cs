using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalOrZipCode { get; set; }

        public string Country { get; set; }

        //Relationships

        public List<User_Address> Users_Addresses { get; set; }
        
    }
}
