using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Simulate a product list
        private List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Fantasy Crunchy Choco Chip Cookies", Description = "Delicious choco chip cookies", ImageUrl = "/images/chocochip.png.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 2, Name = "Peanut Butter Bite Premium Butter Cookies", Description = "Tasty peanut butter cookies", ImageUrl = "/images/peanut.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 3, Name = "Yumitos Chilli Sprinkled Potato Chips", Description = "Spicy and crunchy potato chips", ImageUrl = "/images/chips.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 4, Name = "Healthy Long Life Toned Milk", Description = "Nutritious and long-lasting milk", ImageUrl = "/images/milk1l.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 5, Name = "Raw Mutton Leg, Packaging 5 Kg", Description = "Fresh and tender mutton leg", ImageUrl = "/images/beef.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 6, Name = "Cold Brew Coffee Instant Coffee", Description = "Rich and smooth cold brew coffee", ImageUrl = "/images/coffee.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 7, Name = "SnackAmor Combo Pack of Jowar Stick and Jowar Chips", Description = "Healthy and tasty snack combo", ImageUrl = "/images/snackarmor.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 8, Name = "Neu Farm Unpolished Desi Toor Dal", Description = "Organic and nutritious toor dal", ImageUrl = "/images/neufarm.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 9, Name = "Dog Treats Natural Yak Milk Bars For Small Dogs", Description = "Healthy treats for small dogs", ImageUrl = "/images/dogfood.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
            new Product { Id = 10, Name = "Blended Instant Coffee 50 g Buy 1 Get 1 Free", Description = "Delicious instant coffee, buy 1 get 1 free", ImageUrl = "/images/coffee1get1.png", CurrentPrice = 26.69m, OriginalPrice = 28.66m },
        };

        public IActionResult Index()
        {
            return View(Products);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
