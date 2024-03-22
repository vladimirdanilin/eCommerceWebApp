using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Services;
using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = await _addressService.GetCurrentUserIdAsync();
            var allAddresses = await _addressService.GetUserAddressesByUserIdAsync(userId);
            return View(allAddresses);
        }

        [Authorize]
        public async Task<IActionResult> AddAddress()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAddress(Address address)
        {
            await _addressService.AddAddressAsync(address);

            return View("Index");
        }

        [Authorize]
        public async Task<IActionResult> DeleteAddress(Address address)
        {
            await _addressService.DeleteAddressAsync(address.Id);

            return View("Index");
        }
    }
}
