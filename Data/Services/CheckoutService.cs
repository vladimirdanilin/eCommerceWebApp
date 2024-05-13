
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

        public async Task<Checkout> AddNewCheckoutAsync(int orderId)
        {
            var user = await GetUserAsync();
            var userAddresses = await GetAddressesAsync(user.Id);

            Checkout checkout = new Checkout()
            {
            UserAddresses = userAddresses,
            Order = _context.Orders.FirstOrDefault(o => o.Id == orderId),
            OrderId = orderId
            };

            await _context.Checkouts.AddAsync(checkout);
            await _context.SaveChangesAsync();

            return checkout;            
        }

        public async Task<List<Address>> GetAddressesAsync(int userId)
        {
            var userAddresses = await _context.Users_Addresses.Where(u => u.UserId == userId).Select(u => u.Address).ToListAsync();

            return userAddresses;
        }

        public async Task<User> GetUserAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);

            return user;
        }

        public async Task UpdateOrder(Checkout checkout)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == checkout.OrderId);
            order.ShippingAddressId = checkout.ShippingAddressId;

            await _context.SaveChangesAsync();
        }
    }
}
