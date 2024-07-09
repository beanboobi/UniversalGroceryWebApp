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

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Employee data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Password = "password123",
                    JoinDate = "2024-01-01",
                    Salary = 60000,
                    Role = "Admin"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Password = "password456",
                    JoinDate = "2024-01-15",
                    Salary = 50000,
                    Role = "Employee"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    Password = "password789",
                    JoinDate = "2024-02-01",
                    Salary = 55000,
                    Role = "Employee"
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