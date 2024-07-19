using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Models;
using Microsoft.AspNetCore.Identity;

namespace WebAppProject.Data
{
    public class ApplicationUser : IdentityUser
    {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany(u => u.Employees)
                .HasForeignKey(e => e.UserId);

            // Seed Users data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "john.doe",
                    Password = "password123", // Ideally, this should be hashed
                    Email = "john.doe@example.com",
                    Role = "Admin"
                },
                new User
                {
                    UserId = 2,
                    Username = "jane.smith",
                    Password = "password456", // Ideally, this should be hashed
                    Email = "jane.smith@example.com",
                    Role = "Employee"
                },
                new User
                {
                    UserId = 3,
                    Username = "bob.johnson",
                    Password = "password789", // Ideally, this should be hashed
                    Email = "bob.johnson@example.com",
                    Role = "Employee"
                }
            );

            // Seed Employee data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 110,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Password = "password123",
                    JoinDate = "2024-01-01",
                    Salary = 60000,
                    Role = "Admin",
                    UserId = 1 // Matches the UserId of the corresponding User
                },
                new Employee
                {
                    Id = 112,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Password = "password456",
                    JoinDate = "2024-01-15",
                    Salary = 50000,
                    Role = "Employee",
                    UserId = 2 // Matches the UserId of the corresponding User
                },
                new Employee
                {
                    Id = 113,
                    Name = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    Password = "password789",
                    JoinDate = "2024-02-01",
                    Salary = 55000,
                    Role = "Employee",
                    UserId = 3 // Matches the UserId of the corresponding User
                }
            );

            // Seed GroceryItem data
            modelBuilder.Entity<GroceryItem>().HasData(
                new GroceryItem
                {
                    Id = 1,
                    Name = "Apple",
                    Quantity = 50,
                    Description = "Fresh apples",
                    Price = 0.99M,
                    ImageUrl = "/images/apple.png",
                    Discount = 10,
                    CreatedDate = "2024-07-05",
                    Category = "Fruits"
                },
                new GroceryItem
                {
                    Id = 2,
                    Name = "Banana",
                    Quantity = 100,
                    Description = "Ripe bananas",
                    Price = 0.59M,
                    ImageUrl = "/images/banana.png",
                    Discount = 15,
                    CreatedDate = "2024-07-05",
                    Category = "Fruits"
                },
                new GroceryItem
                {
                    Id = 3,
                    Name = "Orange",
                    Quantity = 75,
                    Description = "Juicy oranges",
                    Price = 1.29M,
                    ImageUrl = "/images/Orange.png",
                    Discount = 5,
                    CreatedDate = "2024-07-05",
                    Category = "Fruits"
                }
            );
        }
    }
}
