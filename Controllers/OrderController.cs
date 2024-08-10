using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<User> _userManager;

        public OrderController(AppDbContext context ,IOrderService orderService, IShoppingCartService shoppingCartService, UserManager<User> userManager)
        {
            _context = context;
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);

            var orderViewModel = new OrderViewModel
            {
                UserAddresses = await _context.UsersAddresses
                    .Where(ua => ua.UserId == user.Id)
                    .Select(ua => ua.Address)
                    .ToListAsync()
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel orderViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var placedOrderId = await _orderService.PlaceOrderAndGetIdAsync(user.Id, orderViewModel.ShippingAddressId);

            await _shoppingCartService.ClearShoppingCartAsync(user.Id);
            ViewBag.OrderId = placedOrderId;

            return View("OrderPlacementConfirmation");
        }

        public async Task<IActionResult> DisplayOrders()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var orders = await _orderService.GetOrdersByUserIdAsync(currentUser.Id);
            return View("Orders", orders);
        }
    }
}
