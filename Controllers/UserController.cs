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
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager; 
        private readonly IUserService _userService;

        public UserController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> DisplayPersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            var userViewModel = await _userService.GetPersonalDataAsync(user.Id);

            return View(userViewModel);
        }

        public async Task<IActionResult> EditUserData()
        {
            var user = await _userManager.GetUserAsync(User);
            var userViewModel = await _userService.GetPersonalDataAsync(user.Id);

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserData(UserViewModel userViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            await _userService.EditUserDataAsync(user.Id, userViewModel);

            return RedirectToAction("DisplayPersonalData");
        }
    }
}