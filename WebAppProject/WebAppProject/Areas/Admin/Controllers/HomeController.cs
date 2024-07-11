using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> AddProduct(GroceryItem item, IFormFile productPicture)
        {
            _logger.LogInformation("AddProduct action called");

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

            ModelState.Remove(nameof(item.ImageUrl));

            if (string.IsNullOrEmpty(item.CreatedDate))
            {
                item.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            _logger.LogInformation($"ImageUrl before validation: {item.ImageUrl}");

            if (ModelState.IsValid)
            {
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
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
            }

            return View(item);
        }

        public IActionResult ManageEmployee()
        {
            var employees = _context.Employee.ToList();
            return View(employees);
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

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEmployee));
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employee.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(ManageEmployee));
        }

        // Edit Employee actions
        public IActionResult EditEmployee(int id)
        {
            var employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View("EmployeeEditForm", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEmployee));
            }
            return View("EmployeeEditForm", employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var item = _context.GroceryItem.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View("ItemEditForm", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(GroceryItem item, IFormFile productPicture)
        {
            // Remove ImageUrl from ModelState to bypass validation for this field
            ModelState.Remove(nameof(item.ImageUrl));

            ModelState.Remove(nameof(item.CreatedDate));
            var originalItem = await _context.GroceryItem.AsNoTracking().FirstOrDefaultAsync(i => i.Id == item.Id);
            if (originalItem != null)
            {
                item.CreatedDate = originalItem.CreatedDate;
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"Model Error: {error.ErrorMessage}");
                    }
                }
                return View("ItemEditForm", item);
            }

            try
            {
                var existingItem = await _context.GroceryItem.FindAsync(item.Id);
                if (existingItem == null)
                {
                    return NotFound();
                }

                // Update the existing item with the new values
                _context.Entry(existingItem).CurrentValues.SetValues(item);

                if (productPicture != null && productPicture.Length > 0)
                {
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

                    existingItem.ImageUrl = "/uploads/" + uniqueFileName;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageItem));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating item: {ex.Message}");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return View("ItemEditForm", item);
            }
        }
        private bool GroceryItemExists(int id)
        {
            return _context.GroceryItem.Any(e => e.Id == id);
        }
    }
}
