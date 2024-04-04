using eCommerceWebApp.Models;

namespace eCommerceWebApp.Data.Services
{
    public class OrderService : IOrderService
    {

        public Task<Order> GetOrderByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> PlaceOrderAsync()
        {

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
