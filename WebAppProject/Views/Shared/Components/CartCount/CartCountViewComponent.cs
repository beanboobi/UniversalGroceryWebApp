using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebAppProject.Helpers;
using WebAppProject.ViewModels;

public class CartCountViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartCountViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetCartSessionKey()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;

        // Optionally, handle cases where userId might be null
        if (string.IsNullOrEmpty(userId))
        {
            // Handle the case where user ID is not available
            return "cart_guest"; // or another default key
        }

        return $"cart_{userId}";
    }

    public IViewComponentResult Invoke()
    {
        var sessionKey = GetCartSessionKey();
        var cartJson = _httpContextAccessor.HttpContext.Session.GetString(sessionKey);
        var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(_httpContextAccessor.HttpContext.Session, sessionKey);
        int itemCount = cart?.Count ?? 0;

        return View(itemCount);
    }
}
