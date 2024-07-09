using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using Microsoft.Extensions.Logging;

namespace WebAppProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageItem()
        {
            var items = _context.GroceryItem.ToList();
            return View(items);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(GroceryItem item, IFormFile productPicture)
        {
            _logger.LogInformation("AddProduct action called");



            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid");

    
                if (productPicture != null && productPicture.Length > 0)
                {
                    _logger.LogInformation("Product picture is not null and has length");

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + productPicture.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productPicture.CopyToAsync(fileStream);
                    }

                    item.ImageUrl = "/uploads/" + uniqueFileName;
                    _logger.LogInformation($"File uploaded to {item.ImageUrl}");
                }
                else
                {
                    // Set a default image if no image is uploaded
                    item.ImageUrl = "/images/default-product.png";
                    _logger.LogInformation("No product picture uploaded, setting default image");
                }

                if (string.IsNullOrEmpty(item.CreatedDate))
                {
                    item.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                try
                {
                    _context.GroceryItem.Add(item);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Item saved to database");
                    return RedirectToAction(nameof(ManageItem));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving item to database");
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            else
            {
                _logger.LogInformation("Model state is not valid");
                // If ModelState is not valid, log the errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
            }

            // If we got this far, something failed; redisplay form
            return View(item);
        }

        public IActionResult ManageEmployee()
        {
            return View();
        }

        public IActionResult ManageCustomerAcc()
        {
            return View();
        }

        public IActionResult ManageWebsite()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View("CustomerEditForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int id)
        {
            var item = _context.GroceryItem.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.GroceryItem.Remove(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(ManageItem));
        }
    }
}
