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

        public async Task<IActionResult> Index(int orderId)
        {
            var checkout = await _checkoutService.AddNewCheckoutAsync(orderId);

            return View(checkout);
        }

        /*public async Task<IActionResult> AddCheckoutData()
        {

        }*/


    }
}
