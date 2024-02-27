using eCommerceWebApp.Data.Services;
using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace eCommerceWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        public async Task<IActionResult> Index()
        {
            var cart = await _shoppingCartService.GetCartAsync(await _shoppingCartService.GetCurrentUserIdAsync());

            return View(cart);
        }
    }
}
