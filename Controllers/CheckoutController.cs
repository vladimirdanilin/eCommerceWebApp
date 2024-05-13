using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using eCommerceWebApp.Models;

namespace eCommerceWebApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        public async Task<IActionResult> Index(int orderId)
        {
            var checkout = await _checkoutService.AddNewCheckoutAsync(orderId);
            return View(checkout);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCheckoutData(Checkout checkout)
        {
            await _checkoutService.UpdateOrder(checkout);

            return View("CheckoutConfirmation");
        }


    }
}
