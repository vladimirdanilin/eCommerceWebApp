using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace eCommerceWebApp.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Order> GetOrderByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task PlaceOrderAsync(int shoppingCartId)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(sc => sc.Id == shoppingCartId);

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateOnly.FromDateTime(DateTime.Now)            
            };

            foreach (var item in shoppingCart.CartItems)
            {
                order.Orders_Products.Add(new Order_Product
                {
                ProductId = item.ProductId,
                Product = item.Product
                });
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Task<Order> CancelOrder()
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrder()
        {
            throw new NotImplementedException();
        }
    }
}
