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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebAppProject.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;  // Define UserManager


        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
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

        public async Task<IActionResult> ManageEmployee()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Role = e.Role,
                    JoinDate = e.JoinDate,
                    Salary = e.Salary
                })
                .ToListAsync();

            return View(employees);
        }

        public async Task<IActionResult> ManageCustomerAcc()
        {
            var users = await _context.Users.ToListAsync();
            var userRolesViewModel = new List<UsersViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Employee") && !roles.Contains("Admin"))
                {
                    var thisViewModel = new UsersViewModel
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email,
                        Roles = roles.ToList() // Convert IList<string> to List<string>
                    };
                    userRolesViewModel.Add(thisViewModel);
                }
            }

            return View(userRolesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _context.Users.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Users.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageCustomerAcc));
        }

        public async Task<IActionResult> EditCustomer(string id)
        {
            var customer = await _context.Users.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(customer);
            var viewModel = new UsersViewModel
            {
                Id = customer.Id,
                Username = customer.UserName,
                Email = customer.Email,
                Roles = roles.ToList()
                
            };

            return View("EditCustomer", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(UsersViewModel viewModel)
        {
            // Remove password from ModelState if it's null or empty
            if (string.IsNullOrEmpty(viewModel.Password))
            {
                ModelState.Remove("Password");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByIdAsync(viewModel.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = viewModel.Username;
            user.Email = viewModel.Email;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(viewModel);
            }

            // Update password if it is provided 
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, viewModel.Password);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(viewModel);
                }
            }

            // Ensure user has "User" role
            if (!await _userManager.IsInRoleAsync(user, "User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            // Update roles if necessary
            if (viewModel.Roles != null && viewModel.Roles.Any())
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRolesAsync(user, viewModel.Roles);
            }

            return RedirectToAction(nameof(ManageCustomerAcc));
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Create the user and set the default password if not provided
            var user = new ApplicationUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email
            };
            var password = string.IsNullOrWhiteSpace(viewModel.Password) ? "DefaultPassword123!" : viewModel.Password;
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }

            // Add the user to the specified role
            var roleResult = await _userManager.AddToRoleAsync(user, viewModel.Role);
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // Delete the user if role assignment fails
                await _userManager.DeleteAsync(user);
                return View(viewModel);
            }

            // Create the employee record
            var employee = new Employee
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Password = password, // Set the password here
                JoinDate = viewModel.JoinDate,
                Salary = viewModel.Salary,
                Role = viewModel.Role,
                ApplicationUserId = user.Id
            };

            _context.Employees.Add(employee);
            var saveResult = await _context.SaveChangesAsync();
            if (saveResult > 0)
            {
                return RedirectToAction(nameof(ManageEmployee));
            }
            else
            {
                ModelState.AddModelError("", "Unable to save the employee data.");
                // Delete the user if saving employee fails
                await _userManager.DeleteAsync(user);
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(employee.ApplicationUserId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    // Instead of returning a view, redirect back to ManageEmployee with an error message
                    TempData["ErrorMessage"] = "Failed to delete the user account.";
                    return RedirectToAction(nameof(ManageEmployee));
                }
            }

            _context.Employees.Remove(employee);

         

            return RedirectToAction(nameof(ManageEmployee));
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        // Edit Employee actions
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(employee.ApplicationUserId);
            var roles = await _userManager.GetRolesAsync(user);
            var viewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Role = roles.FirstOrDefault(), // Assuming one role per user
                JoinDate = employee.JoinDate,
                Salary = employee.Salary
            };
            return View("EmployeeEditForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel viewModel)
        {
            // Remove password from ModelState if it's null or empty
            if (string.IsNullOrEmpty(viewModel.Password))
            {
                ModelState.Remove("Password");
            }

            if (!ModelState.IsValid)
            {
                // Return the same view with the current viewModel to show validation errors
                return View("EmployeeEditForm", viewModel);
            }

            var employee = await _context.Employees.FindAsync(viewModel.Id);
            if (employee == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(employee.ApplicationUserId);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = viewModel.Email; // Usually, username is the same as email
            user.Email = viewModel.Email;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("EmployeeEditForm", viewModel);
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, viewModel.Password);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("EmployeeEditForm", viewModel);
                }
                // Update the password in the Employee model
                employee.Password = viewModel.Password;
            }

            // Update employee details
            employee.Name = viewModel.Name;
            employee.Email = viewModel.Email;
            employee.JoinDate = viewModel.JoinDate;
            employee.Salary = viewModel.Salary;
            employee.Role = viewModel.Role;

            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", "Unable to save the employee data. " + ex.Message);
                return View("EmployeeEditForm", viewModel);
            }

            // Redirect to the employee list view
            return RedirectToAction(nameof(ManageEmployee));
        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            var item = await _context.GroceryItem.FindAsync(id);
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
        

        public IActionResult ManageWebsite()
        {
            var mainBanners = _context.BannerImage.Where(b => b.BannerType == "Main").ToList();

            var viewModel = new ManageWebsiteViewModel
            {
                MainBanners = mainBanners
            };

            return View(viewModel);
        }

        public IActionResult EditBanner(int id)
        {
            var banner = _context.BannerImage.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            var mainBanners = _context.BannerImage.Where(b => b.BannerType == "Main").ToList();

            var viewModel = new ManageWebsiteViewModel
            {
                MainBanners = mainBanners,
                BannerImage = banner
            };

            return View("ManageWebsite", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBannerImage(ManageWebsiteViewModel model, IFormFile bannerPicture)
        {
            if (bannerPicture != null)
            {
                var uniqueFileName = await SaveBannerImageAsync(bannerPicture);
                model.BannerImage.ImagePath = "/uploads/" + uniqueFileName;
            }

            if (model.BannerImage.Id == 0)
            {
                model.BannerImage.CreatedDate = DateTime.Now;
                _context.BannerImage.Add(model.BannerImage);
            }
            else
            {
                var existingBanner = _context.BannerImage.Find(model.BannerImage.Id);
                if (existingBanner == null)
                {
                    return NotFound();
                }
                existingBanner.ImagePath = model.BannerImage.ImagePath ?? existingBanner.ImagePath;
                existingBanner.RedirectUrl = model.BannerImage.RedirectUrl;
                existingBanner.BannerType = model.BannerImage.BannerType; // Ensure the BannerType is updated
                _context.BannerImage.Update(existingBanner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageWebsite));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBannerImage(int id)
        {
            var banner = _context.BannerImage.Find(id);
            if (banner == null)
            {
                return NotFound();
            }
            _context.BannerImage.Remove(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageWebsite));
        }

        private async Task<string> SaveBannerImageAsync(IFormFile bannerPicture)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + bannerPicture.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await bannerPicture.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
    }
}


