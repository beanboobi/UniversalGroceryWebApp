using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace WebAppProject.Data
{
    public static class IdentityInitializer
    {
        private static readonly List<string> Roles = new List<string> { "User", "Employee", "Admin" };

        private static readonly List<(string Username, string Email, string Password, string Role)> Users = new List<(string, string, string, string)>
        {
            ("customer1", "customer1@example.com", "Password123!", "User"),
            ("customer2", "customer2@example.com", "Password123!", "User"),
            ("employee1", "employee1@example.com", "Password123!", "Employee"),
            ("employee2", "employee2@example.com", "Password123!", "Employee"),
            ("admin1", "admin1@example.com", "Password123!", "Admin"),
            ("admin2", "admin2@example.com", "Password123!", "Admin")
        };

        public static async Task InitializeAsync(IServiceProvider services, ILogger logger)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var context = services.GetRequiredService<ApplicationDbContext>();

            await EnsureRolesAsync(roleManager, logger);
            await EnsureUsersAsync(userManager, context, logger);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        logger.LogInformation($"Role '{role}' created successfully.");
                    }
                    else
                    {
                        logger.LogError($"Error creating role '{role}': {result.Errors.First().Description}");
                    }
                }
            }
        }

        private static async Task EnsureUsersAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger logger)
        {
            foreach (var (username, email, password, role) in Users)
            {
                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = username, Email = email };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddToRoleAsync(user, role);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"User '{username}' created and assigned to role '{role}'.");

                            if (role == "Employee" || role == "Admin")
                            {
                                var employee = new Employee
                                {
                                    Name = username,
                                    Email = email,
                                    Password = password,
                                    JoinDate = DateTime.Now,
                                    Salary = 50000, // Adjust as necessary
                                    Role = role,
                                    ApplicationUserId = user.Id
                                };
                                context.Employees.Add(employee);
                                await context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            logger.LogError($"Error assigning role '{role}' to user '{username}': {result.Errors.First().Description}");
                        }
                    }
                    else
                    {
                        logger.LogError($"Error creating user '{username}': {result.Errors.First().Description}");
                    }
                }
            }
        }
    }
}
