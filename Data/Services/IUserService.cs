using ECommerceWebApp.ViewModels;

namespace ECommerceWebApp.Data.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetPersonalDataAsync(int userId);

        Task EditUserDataAsync(int userId, UserViewModel userViewModel);

        string NormalizePhoneNumber(string phoneNumber);
    }
}
