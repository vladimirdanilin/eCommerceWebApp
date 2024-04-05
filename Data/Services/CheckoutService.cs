
using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Data.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public async Task GetAddresses(int userId)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);

            var userAddresses = new List<Address>();

            foreach (var item in _context.Users_Addresses)
            {
                if (item.UserId == user.Id)
                {
                    userAddresses.Add(item.Address);
                }
            }

        }
    }
}
