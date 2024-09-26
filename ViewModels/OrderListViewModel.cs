using ECommerceWebApp.Data.Enums;
using ECommerceWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.ViewModels
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }

        public DateOnly OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public double Total { get; set; }

        public ICollection<OrderProductViewModel> OrderProductViewModels { get; set; }

        public string Username { get; set; }

        public string ShippingAddress { get; set; }
    }
}
