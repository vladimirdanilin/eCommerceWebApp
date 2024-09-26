using ECommerceWebApp.DTOs;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleService(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        { 
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var usersWithRolesDTOs = await (from user in _context.Users
                                        where !_context.UserRoles.Any(ur => ur.UserId == user.Id &&
                                            _context.Roles.Any(r => r.Id == ur.RoleId && r.Name == Roles.SuperAdmin))
                                        join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                        join role in _context.Roles on userRole.RoleId equals role.Id
                                        select new UserDTO
                                        {
                                            Id = user.Id,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Email = user.Email,
                                            RoleName = role.Name
                                        }).ToListAsync();

            return usersWithRolesDTOs;
        }

        public async Task<RoleDTO> GetUserRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));

            if (user == null)
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            RoleDTO roleDTO = new RoleDTO
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };

            return roleDTO;
        }

        public async Task EditUserRolesAsync(int userId, List<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));

            var userRoles = await _userManager.GetRolesAsync(user);

            if (selectedRoles.FirstOrDefault() != userRoles.FirstOrDefault())
            {
                var selectedRole = selectedRoles.Except(userRoles);

                if (await _roleManager.RoleExistsAsync(selectedRole.First()))
                {
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRolesAsync(user, selectedRole);
                }
            }
        }
    }
}
