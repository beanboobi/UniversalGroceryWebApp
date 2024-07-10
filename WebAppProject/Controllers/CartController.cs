// Controllers/CartController.cs
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using WebAppProject.Services;

namespace WebAppProject.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string name, string imageUrl, decimal price, int quantity)
        {
            var cartItem = new CartItem
            {
                ProductId = productId,
                Name = name,
                ImageUrl = imageUrl,
                Price = price,
                Quantity = quantity
            };

            _cartService.AddToCart(cartItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCartItem(int productId, int quantity)
        {
            _cartService.UpdateCartItem(productId, quantity);
            return RedirectToAction("Index");
        }
    }
}
