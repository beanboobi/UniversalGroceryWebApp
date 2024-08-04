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
using WebAppProject.Helpers;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

/*
 * Done By: Kaiboon, Ann, Eugene, Kelvin
 * INFT 3050 Web Programming
 * This is the controller for the index view (Customer Home page), it connects the index view page to the viewmodel, it has 
 * important customer actions/functions such as displaying of the BannerImage, GroceryItems, User info in the Profile page,
 * Allows changing of user password and redirection logic to the other customer pages.
 */

namespace WebAppProject.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // HomeController for Customer Homepage
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

            var mainBanners = _context.BannerImage
                .Where(b => b.BannerType == "MainBanner") // Ensure this matches the seeded data
                .OrderByDescending(b => b.CreatedDate)
                .ToList();

            var viewModel = new HomePageViewModel
            {
                GroceryItems = items,
                MainBanners = mainBanners // Assign the list of main banners
            };

            return View(viewModel);
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

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new UserProfileViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber // Assuming Address is a property of ApplicationUser
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                ViewData["Message"] = "Profile updated successfully";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update profile");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                ViewData["Message"] = "Password changed successfully";
                return View();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
