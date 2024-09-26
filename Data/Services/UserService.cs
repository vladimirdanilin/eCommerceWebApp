using ECommerceWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<UserViewModel> GetPersonalDataAsync(int userId)
        {
            var userViewModel = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                }).FirstOrDefaultAsync();

            return userViewModel;
        }

        public async Task EditUserDataAsync(int userId, UserViewModel userViewModel)
        { 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.PhoneNumber = NormalizePhoneNumber(userViewModel.PhoneNumber);

            await _context.SaveChangesAsync();
        }

        public string NormalizePhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Trim();

            return phoneNumber;
        }
    }
}
