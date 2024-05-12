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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> PlaceOrder(int shoppingCartId)
        {
            var placedOrderId = await _orderService.PlaceOrderAndGetIdAsync(shoppingCartId);
            //RedirectToAction("ClearShoppingCart", "ShoppingCart", new {CartId = shoppingCartId});
            return RedirectToAction("Index", "Checkout", new {orderId = placedOrderId});
        }
    }
}
