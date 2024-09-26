using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.Controllers
{
    [Authorize(Roles = Roles.Customer)]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<User> _userManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService, UserManager<User> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await _shoppingCartService.GetCartByUserIdAsync(user.Id);

            if (cart == null)
            {
                return View();
            }

            var shoppingCartViewModel = new ShoppingCartViewModel(cart.Id, cart.CartItems);

            return View(shoppingCartViewModel);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> AddItemToCart(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            await _shoppingCartService.AddItemToCartAsync(user.Id, productId, quantity);

            return RedirectToAction("Index", "ShoppingCart");
        }

        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _shoppingCartService.RemoveItemFromCartAsync(user.Id, productId);

            return RedirectToAction("Index", "ShoppingCart");
        }

        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> ClearShoppingCart()
        {
            var user = await _userManager.GetUserAsync(User);
            await _shoppingCartService.ClearShoppingCartAsync(user.Id);

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
