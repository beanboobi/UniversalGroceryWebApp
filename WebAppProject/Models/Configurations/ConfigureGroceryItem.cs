using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppProject.Models;

namespace WebAppProject.Configuration
{
    internal class ConfigureGroceryItem : IEntityTypeConfiguration<GroceryItem>
    {
        public void Configure(EntityTypeBuilder<GroceryItem> entity)
        {
            entity.HasData(
                new GroceryItem { Id = 1, Name = "Apple", Quantity = 100, Description = "Fresh Red Apple", Price = 0.5M, Category = "Fruits",  Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/apple.png", },
                new GroceryItem { Id = 2, Name = "Banana", Quantity = 150, Description = "Organic Banana", Price = 0.3M, Category = "Fruits",  Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Banana.jpeg", },
                new GroceryItem { Id = 3, Name = "Carrot", Quantity = 200, Description = "Fresh Carrot", Price = 0.2M, Category = "Vegetables",  Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Carrot.png", },
                new GroceryItem { Id = 4, Name = "Tomato", Quantity = 180, Description = "Organic Tomato", Price = 0.4M, Category = "Vegetables", Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Tomato.jpeg", },
                new GroceryItem { Id = 5, Name = "Milk", Quantity = 100, Description = "Full Cream Milk", Price = 1.5M, Category = "Dairy", Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Full Cream Milk.jpeg", },
                new GroceryItem { Id = 6, Name = "Cheese", Quantity = 50, Description = "Cheddar Cheese", Price = 2.0M, Category = "Dairy", Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Cheddar Cheese.jpeg", },
                new GroceryItem { Id = 7, Name = "Bread", Quantity = 120, Description = "Whole Wheat Bread", Price = 1.0M, Category = "Bakery", Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Whole Wheat Bread.jpeg", },
                new GroceryItem { Id = 8, Name = "Chicken Breast", Quantity = 90, Description = "Boneless Chicken Breast", Price = 3.5M, Category = "Meat", Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Boneless Chicken Breast.jpeg", },
                new GroceryItem { Id = 9, Name = "Salmon", Quantity = 70, Description = "Fresh Salmon Fillet", Price = 10.0M, Category = "Seafood",  Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Fresh Salmon Fillet.jpeg", },
                new GroceryItem { Id = 10, Name = "Rice", Quantity = 300, Description = "Basmati Rice", Price = 1.2M, Category = "Grains",  Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Basmati Rice.jpeg", },
                new GroceryItem { Id = 11, Name = "Pasta", Quantity = 250, Description = "Italian Pasta", Price = 1.1M, Category = "Grains",  Discount = 0, CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"), ImageUrl = "/images/Italian Pasta.jpeg", }
            );
        }
    }
}