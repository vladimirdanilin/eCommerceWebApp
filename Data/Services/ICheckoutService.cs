using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface ICheckoutService
    {
        Task<List<Address>> GetAddressesAsync();

        Task<Checkout> AddNewCheckoutAsync(List<Address> userAddresses);

        Task<User> GetUserAsync();
    }
}
