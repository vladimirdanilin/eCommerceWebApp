using eCommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceWebApp.Data.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(AppDbContext context , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await _context.ShoppingCarts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);

            //If the cart does not exist, create a new one
            if (cart == null)
            {
                await _context.ShoppingCarts.AddAsync(new ShoppingCart()
                {
                    UserId = userId
                });
                await _context.SaveChangesAsync();
                cart = await _context.ShoppingCarts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            }

            //Checking whether the item is in the cart
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                Product product = _context.Products.FirstOrDefault(p => p.Id == productId);
                cart.CartItems.Add(new CartItem()
                {
                    ProductId = productId,
                    Product = product,
                    Quantity = quantity,
                    TotalUnitPrice = Math.Round(quantity * product.Price, 2)
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetCartAsync(int userId)
        {
            var cart = await _context.ShoppingCarts.Include(c => c.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            cart.TotalPrice = 0;
            foreach (var item in cart.CartItems)
            {
                cart.TotalPrice += item.Product.Price * item.Quantity;
            }
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<int> GetCurrentUserIdAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);

            return user.Id;
        }

        public async Task RemoveItemFromCartAsync(int userId, int productId, int quantity)
        {
            var cart = await _context.ShoppingCarts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            var item = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cart.CartItems.Contains(item))
            {
                cart.CartItems.Remove(item);
            }
        }
    }
}
