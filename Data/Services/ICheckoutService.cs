using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface ICheckoutService
    {
        Task<List<Address>> GetAddressesAsync(int userId);

        Task<Checkout> AddNewCheckoutAsync(int orderId);

        Task<User> GetUserAsync();
    }
}
