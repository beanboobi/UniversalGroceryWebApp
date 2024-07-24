using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace WebAppProject.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.GroceryItem

                .OrderByDescending(item => item.Discount)
                .Take(8)
                .Select(item => new GroceryItemViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price - (item.Price * ((decimal)item.Discount / 100)),
                    ImageUrl = item.ImageUrl,
                    OriginalPrice = item.Price, // Assuming original price is the same for now
                    Description = item.Description,
                })
               .ToList();

            return View(items);
        }
        [HttpGet]
        public IActionResult ProductCategory(string category)
        {
            var product = _context.GroceryItem
               .Where(c => c.Category == category)
               .Select(item => new GroceryItemViewModel
               {
                   Id = item.Id,
                   Name = item.Name,
                   Price = item.Price - (item.Price * ((decimal)item.Discount / 100)),
                   ImageUrl = item.ImageUrl,
                   OriginalPrice = item.Price, // Assuming original price is the same for now
                   Description = item.Description,
               })
               .ToList();

            return View(product);
        }


        public IActionResult ProductDetails(int id)
        {
            var product = _context.GroceryItem
                .Where(p => p.Id == id)
                .Select(item => new GroceryItemViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price - (item.Price * ((decimal)item.Discount / 100)),
                    ImageUrl = item.ImageUrl,
                    OriginalPrice = item.Price, // Assuming original price is the same for now
                    Description = item.Description,
                })
                .FirstOrDefault(); ;
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

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult OrderHistory()
        {
            return View();
        }
        public IActionResult OrderDetails()
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
