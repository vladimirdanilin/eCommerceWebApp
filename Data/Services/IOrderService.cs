using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<Order> GetOrderByIdAsync();

        Task<Order> PlaceOrderAsync();

        Task<Order> CancelOrder();

        Task<Order> UpdateOrder();
    }
}
