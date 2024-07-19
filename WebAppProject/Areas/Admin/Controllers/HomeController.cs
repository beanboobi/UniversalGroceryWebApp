using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebAppProject.ViewModels;

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
            var customers = _context.Users.Where(u => u.Role == "Customer").ToList(); // Adjust query to filter by role or any other condition as needed
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Users.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Users.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(ManageCustomerAcc));
        }

        public IActionResult ManageWebsite()
        {
            return View();
        }

        public IActionResult EditCustomer(int id)
        {
            var customer = _context.Users.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new UsersViewModel
            {
                UserId = id,
                Username = customer.Username,
                Email = customer.Email,
                Role = customer.Role
                // Password is not populated here for security reasons
            };

            return View("EditCustomer", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(UsersViewModel viewModel)
        {
            // Remove password from model state to avoid validation issues if not provided
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                // Retrieve the existing user from the database
                var existingUser = await _context.Users.FindAsync(viewModel.UserId);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update properties except password
                existingUser.Username = viewModel.Username;
                existingUser.Email = viewModel.Email;
                existingUser.Role = viewModel.Role;

                // Update password if provided (ensure to hash in production)
                if (!string.IsNullOrEmpty(viewModel.Password))
                {
                    existingUser.Password = viewModel.Password; // This should be hashed in production
                }

                _context.Update(existingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageCustomerAcc)); // Ensure ManageCustomerAcc action exists
            }

            return View("EditCustomer", viewModel);
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
        public async Task<IActionResult> AddEmployee(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Create a new User
                var user = new User
                {
                    Username = viewModel.Email,  // Example: Use email as username or any unique identifier
                    Password = "defaultpassword", // Example: Provide a default password or implement hashing
                    Email = viewModel.Email,
                    Role = viewModel.Role  // Assign role based on employee's role
                };

                // Add and save User first to get the UserId
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Create Employee from ViewModel
                var employee = new Employee
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    JoinDate = viewModel.JoinDate,
                    Salary = viewModel.Salary,
                    Role = viewModel.Role,
                    UserId = user.UserId  // Assign UserId from created User
                };

                // Add Employee to database
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ManageEmployee));
            }
            return View(viewModel);
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

            var viewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Password = employee.Password,
                JoinDate = employee.JoinDate,
                Salary = employee.Salary,
                Role = employee.Role
            };

            return View("EmployeeEditForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel viewModel)
        {
            // Remove password validation temporarily
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var existingEmployee = await _context.Employee.FindAsync(viewModel.Id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Update the properties
                existingEmployee.Name = viewModel.Name;
                existingEmployee.Email = viewModel.Email;
                existingEmployee.JoinDate = viewModel.JoinDate;
                existingEmployee.Salary = viewModel.Salary;
                existingEmployee.Role = viewModel.Role;

                // Update the password only if a new password is provided
                if (!string.IsNullOrEmpty(viewModel.Password))
                {
                    existingEmployee.Password = viewModel.Password; // Remember to hash the password in production
                }

                _context.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEmployee));
            }
            return View("EmployeeEditForm", viewModel);
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
            // Remove validation for the CreatedDate and ImageUrl fields
            ModelState.Remove(nameof(item.CreatedDate));
            ModelState.Remove(nameof(item.ImageUrl));
           

            // Retrieve the original item from the database to preserve the original ImageUrl and CreatedDate
            var originalItem = await _context.GroceryItem.AsNoTracking().FirstOrDefaultAsync(i => i.Id == item.Id);
            if (originalItem == null)
            {
                return NotFound();
            }

            item.CreatedDate = originalItem.CreatedDate;

            // Preserve the original ImageUrl if no new image is uploaded
            if (productPicture == null)
            {
                item.ImageUrl = originalItem.ImageUrl;
                ModelState.Remove(nameof(productPicture));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Only process the image if a new one is uploaded
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

                        item.ImageUrl = "/uploads/" + uniqueFileName;
                    }

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ManageItem));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                // Log model state errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"Model Error: {error.ErrorMessage}");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View("ItemEditForm", item);
        }
        private bool GroceryItemExists(int id)
        {
            return _context.GroceryItem.Any(e => e.Id == id);
        }
    }
}

