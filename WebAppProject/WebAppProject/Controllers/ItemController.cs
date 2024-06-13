using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class ItemController : Controller
    {
        private static List<Item> items = new List<Item>
        {
            new Item { ID = 1, Name = "Apple", Picture = "apple.png", Description = "Orgo Fresh South African Premium Granny", Price = 3.50M, Discount = 3 },
            new Item { ID = 2, Name = "Broccoli", Picture = "broccoli.png", Description = "Orgo Fresh Royal Broccoli", Price = 2.50M, Discount = 2 },
    
        };

        public IActionResult Index()
        {
            return View(items);
        }

        public IActionResult Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.ID == id);
            if (item != null)
            {
                items.Remove(item);
            }
            return RedirectToAction("Index");
        }
    }
}
