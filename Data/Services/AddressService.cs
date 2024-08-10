using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data.Services
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _context;

        public AddressService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAddressAsync(AddressViewModel addressViewModel, int userId)
        {
            var address = new Address
            { 
                Street = addressViewModel.Street,
                City = addressViewModel.City,
                UsersAddresses = new List<UserAddress>
                { 
                    new UserAddress
                    { 
                        UserId = userId
                    }
                }
            };

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int addressId, int userId)
        {
            var userAddress = await _context.UsersAddresses
                .FirstOrDefaultAsync(ua => ua.AddressId == addressId && ua.UserId == userId);

            if (userAddress != null)
            { 
                _context.UsersAddresses.Remove(userAddress);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Address>> GetUserAddressesByUserIdAsync(int userId)
        {
            var userAddresses = await (
                from ua in _context.UsersAddresses
                join a in _context.Addresses on ua.AddressId equals a.Id
                where ua.UserId == userId 
                select a)
                .ToListAsync();

            return userAddresses;
        }
    }
}
