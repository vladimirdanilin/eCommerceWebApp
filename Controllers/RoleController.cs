using ECommerceWebApp.Data;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(AppDbContext context, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> UserList()
        {
            var usersWithRoles = await (from user in _context.Users
                                        where !_context.UserRoles.Any(ur => ur.UserId == user.Id && 
                                            _context.Roles.Any(r => r.Id == ur.RoleId && r.Name == "SuperAdmin"))
                                        join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                        join role in _context.Roles on userRole.RoleId equals role.Id
                                        select new UserViewModel
                                        {
                                            Id = user.Id,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Email = user.Email,
                                            RoleName = role.Name
                                        }).ToListAsync();

            return View(usersWithRoles);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            { 
                return NotFound();
            }    

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            RoleViewModel roleViewModel = new RoleViewModel
            {
                UserId = userId,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };

            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 1 && !roles.Contains("SuperAdmin"))
            {
                var selectedRole = roles.Except(userRoles);

                if (await _roleManager.RoleExistsAsync(selectedRole.First()))
                {
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRolesAsync(user, selectedRole);

                    return RedirectToAction("UserList");
                }
            }

            var allRoles = _roleManager.Roles.ToList();

            RoleViewModel roleViewModel = new RoleViewModel
            {
                UserId = userId,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };

            return View(roleViewModel);
        }

    }
}
