using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly UserManager<User> _userManager;

        public AddressController(IAddressService addressService, UserManager<User> userManager)
        {
            _addressService = addressService;
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var allAddresses = await _addressService.GetUserAddressesByUserIdAsync(currentUser.Id);

            var addressViewModels = allAddresses.Select(address => new AddressViewModel
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
            }).ToList();

            return View(addressViewModels);
        }

        [Authorize (Roles = Roles.Customer)]
        public async Task<IActionResult> AddAddress()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> AddAddress(AddressViewModel addressViewModel)
        {
            if (!ModelState.IsValid)
            { 
                return View(addressViewModel);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            await _addressService.AddAddressAsync(addressViewModel, currentUser.Id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _addressService.DeleteAddressAsync(addressId, currentUser.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
