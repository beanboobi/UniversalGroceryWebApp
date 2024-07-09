using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.ViewModels;
using System.Net.Mail;


namespace WebAppProject.Controllers;

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
                    Name = item.Name,
                    Price = item.Price - (item.Price * ((decimal)item.Discount / 100)),
                    ImageUrl = item.ImageUrl,
                    OriginalPrice = item.Price, // Assuming original price is the same for now
                })
           .ToList();

        return View(items);
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

