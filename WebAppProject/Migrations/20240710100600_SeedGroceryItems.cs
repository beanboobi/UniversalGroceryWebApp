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
                VALUES 
                ('Fantasy Crunchy Choco Chip Cookies', 100, 'Delicious crunchy choco chip cookies.', 26.69, '/images/chocochip.png', 10, GETDATE()),
                ('Peanut Butter Bite Premium Butter Cookies', 100, 'Premium butter cookies with peanut butter.', 26.69, '/images/peanut.png', 15, GETDATE()),
                ('Yumitos Chilli Sprinkled Potato Chips', 100, 'Spicy chilli sprinkled potato chips.', 26.69, '/images/chips.png', 20, GETDATE()),
                ('Healthy Long Life Toned Milk', 100, 'Healthy long life toned milk.', 26.69, '/images/milk1l.png', 5, GETDATE()),
                ('Raw Mutton Leg, Packaging 5 Kg', 100, 'Fresh raw mutton leg, 5 Kg packaging.', 26.69, '/images/beef.png', 25, GETDATE()),
                ('Cold Brew Coffee Instant Coffee', 100, 'Instant cold brew coffee.', 26.69, '/images/coffee.png', 30, GETDATE()),
                ('SnackAmor Combo Pack of Jowar Stick and Jowar Chips', 100, 'Combo pack of Jowar stick and chips.', 26.69, '/images/snackarmor.png', 35, GETDATE()),
                ('Neu Farm Unpolished Desi Toor Dal', 100, 'Unpolished Desi Toor Dal.', 26.69, '/images/neufarm.png', 40, GETDATE()),
                ('Dog Treats Natural Yak Milk Bars For Small Dogs', 100, 'Natural Yak Milk Bars for small dogs.', 26.69, '/images/dogfood.png', 45, GETDATE()),
                ('Blended Instant Coffee 50 g Buy 1 Get 1 Free', 100, 'Blended instant coffee, buy 1 get 1 free.', 26.69, '/images/coffee1get1.png', 50, GETDATE());
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM GroceryItem WHERE Name IN ('Apple', 'Banana', 'Orange')");
        }
    }

}