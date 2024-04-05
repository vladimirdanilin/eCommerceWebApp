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
            await _orderService.PlaceOrderAsync(shoppingCartId);
            return RedirectToAction("Index", "Product");
        }
    }
}
