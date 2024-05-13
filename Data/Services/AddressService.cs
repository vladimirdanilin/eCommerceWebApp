using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Data.Services
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAddressAsync(Address address)
        {
            var userId = await GetCurrentUserIdAsync();
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            var addressId = (await _context.Addresses.FirstOrDefaultAsync(a => a.Street == address.Street && a.City == address.City && a.Country == address.Country && a.PostalOrZipCode == address.PostalOrZipCode)).Id;
            await _context.Users_Addresses.AddAsync(
                new User_Address
                {
                    UserId = userId,
                    AddressId = addressId
                });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int addressId)
        {
            int userId = await GetCurrentUserIdAsync();
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);

            foreach (var user_address in _context.Users_Addresses)
            {
                if ((user_address.AddressId == addressId) && (user_address.UserId == userId))
                { 
                    _context.Users_Addresses.Remove(user_address);
                }
            }
            await _context.SaveChangesAsync();
        }

        public Task<Address> EditAddressAsync(Address address)
        {
            throw new NotImplementedException();
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
            return address;
        }

        public async Task<List<Address>> GetUserAddressesByUserIdAsync(int userId)
        {
            List<Address> userAddresses = new List<Address>();

            var userAddressRecords = await _context.Users_Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync();

            foreach (var userAddressRecord in userAddressRecords)
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == userAddressRecord.AddressId);

                if (address != null)
                {
                    userAddresses.Add(address);
                }
            }
            return userAddresses;
        }

        public async Task<int> GetCurrentUserIdAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);

            return user.Id;
        }
    }
}
