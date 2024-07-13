using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebAppProject.Data;
using WebAppProject.ViewModels;
using System.Collections.Generic;
using System.Linq;
using WebAppProject.Helpers;
using WebAppProject.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Cart()
    {
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") ?? new List<CartItem>();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") ?? new List<CartItem>();

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

        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        return RedirectToAction("Cart");
    }

    [HttpPost]
    public IActionResult Remove(int itemId)
    {
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") ?? new List<CartItem>();

        var cartItem = cart.FirstOrDefault(x => x.Item.Id == itemId);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
            HttpContext.Session.SetObjectAsJson("cart", cart);
        }

        return RedirectToAction("Cart"); // Redirect to the cart view
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


