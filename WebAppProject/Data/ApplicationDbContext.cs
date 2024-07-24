using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Models; // Ensure this namespace is correct

namespace WebAppProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Add additional properties if needed
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryItem> GroceryItem { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employee { get; set; }
    }
}
