using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceWebApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        public async Task<IActionResult> Index()
        {
            var userAddresses = await _checkoutService.GetAddressesAsync();
            var checkout = await _checkoutService.AddNewCheckoutAsync(userAddresses);

            return View(checkout);
        }
    }
}
