using ECommerceWebApp.Models;

namespace ECommerceWebApp.DTOs
{
    public class OrderDTO
    {
        public List<Address> UserAddresses { get; set; }

        public int ShippingAddressId { get; set; }
    }
}
