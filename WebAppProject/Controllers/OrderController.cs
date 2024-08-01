using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppProject.Data;
using WebAppProject.ViewModels;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
    public async Task<IActionResult> OrderHistory()
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var orders = await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                TotalItems = o.OrderItems.Sum(oi => oi.Quantity),
                TotalPrice = o.OrderItems.Sum(oi => oi.Price * oi.Quantity)
            })
            .ToListAsync();

        return View(orders);
    }

    [Authorize]
    public async Task<IActionResult> OrderDetails(int id)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id && o.CustomerId == customerId);

        if (order == null)
        {
            return NotFound();
        }

        var orderDetailViewModel = new OrderDetailViewModel
        {
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
            {
                ItemId = oi.ProductId,
                ItemName = _context.GroceryItem.FirstOrDefault(p => p.Id == oi.ProductId)?.Name,
                Price = oi.Price,
                Quantity = oi.Quantity,
                ImageUrl = _context.GroceryItem.FirstOrDefault(p => p.Id == oi.ProductId)?.ImageUrl,
            }).ToList()
        };

        return View(orderDetailViewModel);
    }
}
