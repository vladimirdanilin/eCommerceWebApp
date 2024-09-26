using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

            var orderDTO = await _orderService.GetOrderDTOAsync(user);

            var orderViewModel = new OrderViewModel
            {
                UserAddresses = orderDTO.UserAddresses
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

        public async Task<IActionResult> DisplayUserOrders()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var orders = await _orderService.GetOrdersByUserIdAsync(currentUser.Id);
            return View("Orders", orders);
        }

        [Authorize(Roles = Roles.AdminRoles)]
        public async Task<IActionResult> DisplayAllOrders()
        {
            var allOrders = await _orderService.GetAllOrdersAsync();
            return View("Orders", allOrders);
        }

        [Authorize]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var orderListViewModel = await _orderService.GetOrderDetailsAsync(orderId);

            if (orderListViewModel == null)
            {
                return NotFound();
            }

            return View("OrderDetails", orderListViewModel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.AdminRoles)]
        public async Task<IActionResult> EditStatus(int orderId, OrderStatus orderStatus)
        {
            var result = await _orderService.EditOrderStatusAsync(orderId, orderStatus);

            if (result == false)
            {
                return NotFound();
            }

            return RedirectToAction("DisplayAllOrders");
        }

        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);

            if (result == false)
            {
                return NotFound();
            }

            return RedirectToAction("DisplayUserOrders");
        }
    }
}
