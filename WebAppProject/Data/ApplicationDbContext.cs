using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Configuration;
using WebAppProject.Configurations;
using WebAppProject.Models; // Ensure this namespace is correct

namespace WebAppProject.Data
{
    public class ApplicationDBContext : IdentityUser
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

        public DbSet<BannerImage> BannerImage { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new ConfigureGroceryItem());
            //modelBuilder.ApplyConfiguration(new InitializeAsync());
            //modelBuilder.ApplyConfiguration(new ConfigureEmployee());
            modelBuilder.ApplyConfiguration(new ConfigureBannerImage());

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ApplicationUser)
                .WithMany(a => a.Employees)
                .HasForeignKey(e => e.ApplicationUserId);
        }
        
    }
}

