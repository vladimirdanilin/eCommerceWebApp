using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IShoppingCartService
    {
        Task<int> GetCurrentUserIdAsync();

        Task<ShoppingCart> GetCartAsync(int userId);

        Task AddItemToCartAsync(int userId, int productId, int quantity);

        Task RemoveItemFromCartAsync(int userId, int productId, int quantity);
    }
}
