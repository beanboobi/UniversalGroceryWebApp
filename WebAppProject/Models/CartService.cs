// Services/CartService.cs
using System.Collections.Generic;
using System.Linq;
using WebAppProject.Models;

namespace WebAppProject.Services
{
    public class CartService
    {
        private readonly List<CartItem> _cartItems;

        public CartService()
        {
            _cartItems = new List<CartItem>();
        }

        public void AddToCart(CartItem item)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cartItems.Add(item);
            }
        }

        public void RemoveFromCart(int productId)
        {
            var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _cartItems.Remove(item);
            }
        }

        public void UpdateCartItem(int productId, int quantity)
        {
            var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }

        public decimal GetTotal()
        {
            return _cartItems.Sum(i => i.Price * i.Quantity);
        }
    }
}
