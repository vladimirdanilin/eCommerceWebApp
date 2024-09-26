using ECommerceWebApp.Models;

namespace ECommerceWebApp.ViewModels
{
    public class OrderProductViewModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
