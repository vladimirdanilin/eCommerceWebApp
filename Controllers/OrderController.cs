using eCommerceWebApp.Data;
using eCommerceWebApp.Data.Services;
using eCommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderController(IOrderService orderService, IShoppingCartService shoppingCartService)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> PlaceOrder(int shoppingCartId)
        {
            var placedOrderId = await _orderService.PlaceOrderAndGetIdAsync(shoppingCartId);

            await _shoppingCartService.ClearShoppingCartAsync(shoppingCartId);

            return RedirectToAction("Index", "Checkout", new {orderId = placedOrderId});
        }
    }
}
