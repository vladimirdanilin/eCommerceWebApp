using eCommerceWebApp.Data.Services;
using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> AddItemToCart(int productId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                int userId = await _shoppingCartService.GetCurrentUserIdAsync();
                await _shoppingCartService.AddItemToCartAsync(userId, productId, 1);
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                int userId = await _shoppingCartService.GetCurrentUserIdAsync();
                await _shoppingCartService.RemoveItemFromCartAsync(userId, productId, 1);
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public async Task<IActionResult> ClearShoppingCart(int shoppingCartId)
        { 
            await _shoppingCartService.ClearShoppingCartAsync(shoppingCartId);

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
