
using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Data.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            var userAddresses = await _context.Users_Addresses.Where(u => u.UserId == GetUserAsync().Id).Select(u => u.Address).ToListAsync();

            return userAddresses;
        }

        public async Task<Checkout> AddNewCheckoutAsync(List<Address> userAddresses)
        {
            Checkout checkout = new Checkout();
            checkout.UserAddresses = userAddresses;
            
            await _context.Checkouts.AddAsync(checkout);
            await _context.SaveChangesAsync();

            return checkout;            
        }

        public async Task<User> GetUserAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);

            return user;
        }
    }
}
