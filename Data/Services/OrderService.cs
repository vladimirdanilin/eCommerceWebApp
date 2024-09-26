using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.DTOs;
using ECommerceWebApp.Models;
using ECommerceWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace ECommerceWebApp.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderListViewModel>> GetOrdersByUserIdAsync(int userId)
        {
            var orderListViewModels = await _context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderListViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Total = o.Total,
                    ShippingAddress = o.ShippingAddress.City + " " + o.ShippingAddress.Street,
                    OrderProductViewModels = o.OrdersProducts.Select(op => new OrderProductViewModel
                    {
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        ProductName = op.Product.Name,
                        Quantity = op.Quantity
                    }).ToList()
                }).ToListAsync();

            return orderListViewModels;
        }

        public async Task<OrderDTO> GetOrderDTOAsync(User user)
        {
            var orderDTO = new OrderDTO
            {
                UserAddresses = await _context.UsersAddresses
                        .Where(ua => ua.UserId == user.Id)
                        .Select(ua => ua.Address)
                        .ToListAsync()
            };

            return orderDTO;
        }

        public async Task<int> PlaceOrderAndGetIdAsync(int userId, int selectedAddressId)
        {
            var selectedAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == selectedAddressId);

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            double total = shoppingCart.CartItems.Sum(item => item.TotalUnitPrice);

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrdersProducts = shoppingCart.CartItems.Select(item => new OrderProduct
                { 
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Quantity = item.Quantity
                }).ToList(),
                ShippingAddress = selectedAddress,
                ShippingAddressId = selectedAddressId,
                Status = Enums.OrderStatus.Pending,
                Total = total
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task<IEnumerable<OrderListViewModel>> GetAllOrdersAsync()
        {
            var orderListViewModels = await _context.Orders
                .Select(o => new OrderListViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Total = o.Total,
                    Username = o.User.UserName,
                    ShippingAddress = o.ShippingAddress.City + " " + o.ShippingAddress.Street,
                    OrderProductViewModels = o.OrdersProducts.Select(op  => new OrderProductViewModel
                    { 
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        ProductName = op.Product.Name,
                        Quantity = op.Quantity
                    }).ToList()
                }).ToListAsync();

            return orderListViewModels;
        }

        public async Task<OrderListViewModel> GetOrderDetailsAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            var orderListViewModel = await _context.Orders
                .Where(o => o.Id == orderId)
                .Select(o => new OrderListViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Total = o.Total,
                    ShippingAddress = o.ShippingAddress.City + " " + o.ShippingAddress.Street,
                    OrderProductViewModels = o.OrdersProducts.Select(op => new OrderProductViewModel
                    {
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        ProductName = op.Product.Name,
                        Quantity = op.Quantity
                    }).ToList()
                }).FirstOrDefaultAsync();

            return orderListViewModel;
        }

        public async Task<bool> EditOrderStatusAsync(int orderId, OrderStatus orderStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            order.Status = orderStatus;
            _context.Update(order);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            order.Status = Enums.OrderStatus.Cancelled;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
