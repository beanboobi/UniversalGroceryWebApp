using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedGroceryItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO GroceryItem (Name, Quantity, Description, Price, ImageUrl, Discount, CreatedDate)
                               VALUES ('Apple', 50, 'Fresh apples', 0.99, '/images/snackarmor.png', 10, '2024-07-05')");

            migrationBuilder.Sql(@"INSERT INTO GroceryItem (Name, Quantity, Description, Price, ImageUrl, Discount, CreatedDate)
                               VALUES ('Banana', 100, 'Ripe bananas', 0.59, '/images/snackarmor.png', 15, '2024-07-05')");

            migrationBuilder.Sql(@"INSERT INTO GroceryItem (Name, Quantity, Description, Price, ImageUrl, Discount, CreatedDate)
                               VALUES ('Orange', 75, 'Juicy oranges', 1.29, '/images/snackarmor.png', 5, '2024-07-05')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM GroceryItem WHERE Name IN ('Apple', 'Banana', 'Orange')");
        }
    }

}
