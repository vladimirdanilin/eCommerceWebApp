using ECommerceWebApp.Models;

namespace ECommerceWebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }

        public List<CartItem>? CartItems { get; set; }

        public double TotalPrice { get; set; }

        public ShoppingCartViewModel(int id, List<CartItem> cartItems) 
        {
            Id = id;
            CartItems = cartItems;
            CalculateCartTotal();
        }

        public void CalculateCartTotal()
        {
            TotalPrice = 0;
            foreach (var item in CartItems)
            {
                TotalPrice += item.TotalUnitPrice;
            }
            TotalPrice = Math.Round(TotalPrice, 2);
        }
    }
}
