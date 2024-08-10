using ECommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Data.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _context;

        public ShoppingCartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart;
        }

        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId);
            Product product =  await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            //If the cart does not exist, create a new one
            if (cart == null)
            {
                await _context.ShoppingCarts.AddAsync(new ShoppingCart()
                {
                    UserId = userId
                });

                await _context.SaveChangesAsync();
            }

            cart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            //Checking whether the item is in the cart
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                cartItem.TotalUnitPrice = Math.Round(cartItem.Quantity * product.Price, 2);
                _context.CartItems.Update(cartItem);
            }
            else
            {
                if (product != null)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Product = product,
                        Quantity = quantity,
                        TotalUnitPrice = Math.Round(quantity * product.Price, 2)
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearShoppingCartAsync(int userId)
        {
            var shoppingCartToRemove = await _context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == userId);
            _context.ShoppingCarts.Remove(shoppingCartToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem.Quantity > 0)
            {
                cartItem.Quantity --;
                cartItem.TotalUnitPrice = Math.Round(cartItem.Quantity * cartItem.Product.Price, 2);
                _context.CartItems.Update(cartItem);
            }
            if (cartItem.Quantity == 0)
            { 
                cart.CartItems.Remove(cartItem);
                _context.CartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}
