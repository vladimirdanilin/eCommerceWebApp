using Microsoft.AspNetCore.Identity;

namespace ECommerceWebApp.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }

        public string? ProfilePictureURL { get; set; }

        public List<Order>? Orders { get; set; }

        //Relationships

        public List<UserAddress>? UsersAddresses { get; set; }
    }
}