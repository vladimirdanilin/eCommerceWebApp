using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync();

        Task<Order> GetOrderByIdAsync();

        Task<int> PlaceOrderAndGetIdAsync(int shoppingCartId);

        Task<Order> CancelOrderAsync();

        Task<Order> UpdateOrderAsync();

        Task<int> GetCurrentUserIdAsync();
    }
}
