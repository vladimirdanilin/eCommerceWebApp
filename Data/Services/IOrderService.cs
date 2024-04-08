using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<Order> GetOrderByIdAsync();

        Task<int> PlaceOrderAndGetIdAsync(int shoppingCartId);

        Task<Order> CancelOrder();

        Task<Order> UpdateOrder();
    }
}
