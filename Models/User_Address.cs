namespace eCommerceWebApp.Models
{
    public class User_Address
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
