namespace ECommerceWebApp.Models
{
    public class UserAddress
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
