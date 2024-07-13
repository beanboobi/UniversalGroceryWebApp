using Microsoft.EntityFrameworkCore;
using WebAppProject.Models; // Ensure this namespace is correct

namespace WebAppProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryItem> GroceryItem { get; set; }
        public DbSet<Users> Users { get; set; } 
    }
}