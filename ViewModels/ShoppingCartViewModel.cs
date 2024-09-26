using ECommerceWebApp.Models;

namespace ECommerceWebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }

        public List<CartItem>? CartItems { get; set; } = new List<CartItem>();

        public double TotalPrice 
        {
            get
            {
                return Math.Round(CartItems?.Sum(item => item.TotalUnitPrice) ?? 0, 2);
            }
        }

        public ShoppingCartViewModel(int id, List<CartItem> cartItems) 
        {
            Id = id;
            CartItems = cartItems;
        }
    }
}
