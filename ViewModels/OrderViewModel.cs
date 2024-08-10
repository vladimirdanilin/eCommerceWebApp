using ECommerceWebApp.Models;

namespace ECommerceWebApp.ViewModels
{
    public class OrderViewModel
    {
        public List<Address> UserAddresses { get; set; }

        public int ShippingAddressId { get; set; }
    }
}
