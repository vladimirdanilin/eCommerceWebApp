using ECommerceWebApp.DTOs;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;

namespace ECommerceWebApp.Data.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        Task<RoleDTO> GetUserRolesAsync(int userId);

        Task EditUserRolesAsync(int userId, List<string> selectedRoles);
    }
}
