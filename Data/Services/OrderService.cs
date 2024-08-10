using ECommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerceWebApp.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).Include(o => o.ShippingAddress).ToListAsync();
        }

        public async Task<int> PlaceOrderAndGetIdAsync(int userId, int selectedAddressId)
        {
            var selectedAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == selectedAddressId);

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrdersProducts = shoppingCart.CartItems.Select(item => new OrderProduct
                { 
                    ProductId = item.ProductId,
                    Product = item.Product
                }).ToList(),
                ShippingAddress = selectedAddress,
                ShippingAddressId = selectedAddressId
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
