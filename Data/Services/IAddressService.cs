using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;

namespace ECommerceWebApp.Data.Services
{
    public interface IAddressService
    {
        Task<List<Address>> GetUserAddressesByUserIdAsync(int userId);

        Task AddAddressAsync(AddressViewModel addressViewModel, int userId);

        Task DeleteAddressAsync(int addressId, int userId);
    }
}
