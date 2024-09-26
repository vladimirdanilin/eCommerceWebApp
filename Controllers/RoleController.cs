using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Controllers
{
    [Authorize(Roles = Roles.SuperAdmin)]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(IRoleService roleService, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> UserList()
        {
            var usersWithRolesDTOs = await _roleService.GetAllUsersAsync();

            var usersWithRolesViewModels = usersWithRolesDTOs.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.RoleName
            }).ToList();

            return View(usersWithRolesViewModels);
        }

        public async Task<IActionResult> Edit(int userId)
        {
            var roleDTO = await _roleService.GetUserRolesAsync(userId);

            if (roleDTO == null)
            {
                return NotFound();
            }

            var roleViewModel = new RoleViewModel
            {
                UserId = roleDTO.UserId,
                UserEmail = roleDTO.UserEmail,
                UserRoles = roleDTO.UserRoles,
                AllRoles = roleDTO.AllRoles
            };

            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int userId, List<string> selectedRoles)
        {
            await _roleService.EditUserRolesAsync(userId, selectedRoles);

            return RedirectToAction("UserList");
        }
            
    }
}
