using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IAddressService
    {
        Task<Address> GetAddressByIdAsync(int addressId);

        Task<List<Address>> GetUserAddressesByUserIdAsync(int userId);

        Task AddAddressAsync(Address address);

        Task<Address> EditAddressAsync(Address address);

        Task DeleteAddressAsync(int addressId);

        Task<int> GetCurrentUserIdAsync();

    }
}
