using ECommerceWebApp.Models;

namespace ECommerceWebApp.Data.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<int> PlaceOrderAndGetIdAsync(int userId, int selectedAddressId);
    }
}
