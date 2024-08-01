using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebAppProject.Data;
using WebAppProject.ViewModels;
using System.Collections.Generic;
using System.Linq;
using WebAppProject.Helpers;
using WebAppProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly OrderHelper _orderHelper;

    public CartController(ApplicationDbContext context, OrderHelper orderHelper)
    {
        _context = context;
        _orderHelper = orderHelper;
    }

    private string GetCartSessionKey()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        Console.WriteLine("User ID: " + userId); // Check if user ID is being retrieved correctly
        return $"cart_{userId}";
    }

    public IActionResult Cart()
    {
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, GetCartSessionKey()) ?? new List<CartItem>();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        var cartSessionKey = GetCartSessionKey();
        Console.WriteLine("Session Key: " + cartSessionKey);
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, GetCartSessionKey()) ?? new List<CartItem>();

        var item = _context.GroceryItem.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        var cartItem = cart.FirstOrDefault(i => i.Item.Id == id);
        if (cartItem == null)
        {
            var itemViewModel = MapToViewModel(item); // Map GroceryItem to GroceryItemViewModel
            cart.Add(new CartItem { Item = itemViewModel, Quantity = 1 });
        }
        else
        {
            cartItem.Quantity++;
        }

        SessionHelper.SetObjectAsJson(HttpContext.Session, GetCartSessionKey(), cart);
        return RedirectToAction("Cart");
    }

    [HttpPost]
    public IActionResult Remove(int itemId)
    {
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, GetCartSessionKey()) ?? new List<CartItem>();

        var cartItem = cart.FirstOrDefault(x => x.Item.Id == itemId);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
            HttpContext.Session.SetObjectAsJson("cart", cart);
        }

        return RedirectToAction("Cart"); // Redirect to the cart view
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SaveOrder()
    {
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, GetCartSessionKey()) ?? new List<CartItem>();
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _orderHelper.SaveOrderAsync(userId, cart);

        // Clear the cart after saving the order
        HttpContext.Session.Remove(GetCartSessionKey());

        return RedirectToAction("Cart"); // You should have an order confirmation view
    }


    private GroceryItemViewModel MapToViewModel(GroceryItem item)
    {
        return new GroceryItemViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price - (item.Price * ((decimal)item.Discount / 100)),
            ImageUrl = item.ImageUrl,
            OriginalPrice = item.Price, // Assuming original price is the same for now
            Description = item.Description,
        };
    }



}


