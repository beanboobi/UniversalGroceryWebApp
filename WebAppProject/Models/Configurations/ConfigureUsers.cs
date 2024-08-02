using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebAppProject.Models;
using WebAppProject.Data;

public class IdentityInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        // First, create roles if they don't exist
        string[] roleNames = { "Admin", "Employee", "Customer" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    logger.LogError($"Error creating role '{roleName}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }

        // Then, create users for each role
        await CreateUser(userManager, "admin1@example.com", "Test123!", "Admin", logger);
        await CreateUser(userManager, "admin2@example.com", "Test123!", "Admin", logger);
        await CreateUser(userManager, "employee1@example.com", "Test123!", "Employee", logger);
        await CreateUser(userManager, "employee2@example.com", "Test123!", "Employee", logger);
        await CreateUser(userManager, "customer1@example.com", "Test123!", "Customer", logger);
        await CreateUser(userManager, "customer2@example.com", "Test123!", "User", logger);
        await CreateUser(userManager, "customer2@example.com", "Test123!", "User", logger);

    }

    private static async Task CreateUser(UserManager<ApplicationUser> userManager, string email, string password, string role,ILogger logger)
    {
        if (userManager.FindByEmailAsync(email).Result == null)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                logger.LogInformation($"User '{email}' created successfully and added to role '{role}'.");
            }
            else
            {
                logger.LogInformation($"User '{password}'");
                logger.LogError($"Error creating user '{email}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            
        }
    }

    public static async Task CreateEmployee(IServiceProvider serviceProvider, ILogger logger)
    {
        // Assume you already have a method to obtain the DbContext
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var adminRoleId = await context.Roles
        .Where(r => r.Name == "Admin")
        .Select(r => r.Id)
        .FirstOrDefaultAsync();

        var adminUserId = await context.UserRoles
            .Where(ur => ur.RoleId == adminRoleId)
            .Select(ur => ur.UserId)
            .FirstOrDefaultAsync();

        var newEmployee = new Employee
        {
            Name = "user1",
            Email = "user1@example.com",
            Password = "password",
            JoinDate = DateTime.Now,
            Salary = 50000,
            Role = "Employee",
            ApplicationUserId = adminUserId
        };

        // Add the new employee to the context
        await context.Employees.AddAsync(newEmployee);

        try
        {
            // Save changes to the database
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error when adding employee: {ex.Message}");
            // Handle or rethrow the exception as needed
        }
    }

}
