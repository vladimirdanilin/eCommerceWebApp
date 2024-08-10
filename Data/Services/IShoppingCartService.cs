using ECommerceWebApp.Models;

namespace ECommerceWebApp.Data.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartByUserIdAsync(int userId);

        Task AddItemToCartAsync(int userId, int productId, int quantity);

        Task RemoveItemFromCartAsync(int userId, int productId);

        Task ClearShoppingCartAsync(int shoppingCartId);
    }
}
