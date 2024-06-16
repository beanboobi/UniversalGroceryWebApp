using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers;

public class LoginController : Controller
{
    private readonly ApplicationDbContext _context;

    public LoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: /Home/Login
    [HttpPost]
    public IActionResult Login(Login model)
    {
        if (!ModelState.IsValid)
        {
            // Debugging point: inspect ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            // Place breakpoint here to inspect errors
            return View("~/Views/Home/Login.cshtml", model);
        }
        if (ModelState.IsValid)
        {
            // Simulate a simple login check (without real password hashing or security checks)
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                // Login successful, redirect to home or dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }
        }

        // If we got this far, something failed, redisplay form
        return View("~/Views/Home/Login.cshtml", model);
    }
}
