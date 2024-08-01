using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Helpers
{
    public class OrderHelper
    {
        private readonly ApplicationDbContext _context;

        public OrderHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveOrderAsync(string customerId, List<CartItem> cartItems)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                CustomerId = customerId,
                OrderItems = cartItems.Select(cartItem => new OrderItem
                {
                    ProductId = cartItem.Item.Id,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Item.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }

}
