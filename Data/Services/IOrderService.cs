using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using ECommerceWebApp.DTOs;

namespace ECommerceWebApp.Data.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListViewModel>> GetOrdersByUserIdAsync(int userId);

        Task<int> PlaceOrderAndGetIdAsync(int userId, int selectedAddressId);

        Task<OrderDTO> GetOrderDTOAsync(User user);

        Task<IEnumerable<OrderListViewModel>> GetAllOrdersAsync();

        Task<OrderListViewModel> GetOrderDetailsAsync(int orderId);

        Task<bool> EditOrderStatusAsync(int orderId, OrderStatus orderStatus);

        Task<bool> CancelOrderAsync(int orderId);
    }
}
