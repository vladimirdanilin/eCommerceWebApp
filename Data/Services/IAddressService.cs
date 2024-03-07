using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IAddressService
    {
        Task<Address> GetAddressByIdAsync(int id);

        Task<List<Address>> GetUserAddressesByUserIdAsync(int userId);

        Task<Address> AddAddressAsync(Address address);

        Task<Address> EditAddressAsync(Address address);

        Task<Address> DeleteAddressAsync(int addressId);

    }
}
