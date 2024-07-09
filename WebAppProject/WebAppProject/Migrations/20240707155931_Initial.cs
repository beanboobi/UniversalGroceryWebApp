﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroceryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryItem", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Email", "JoinDate", "Name", "Password", "Role", "Salary" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "2024-01-01", "John Doe", "password123", "Admin", 60000 },
                    { 2, "jane.smith@example.com", "2024-01-15", "Jane Smith", "password456", "Employee", 50000 },
                    { 3, "bob.johnson@example.com", "2024-02-01", "Bob Johnson", "password789", "Employee", 55000 }
                });

            migrationBuilder.InsertData(
                table: "GroceryItem",
                columns: new[] { "Id", "Category", "CreatedDate", "Description", "Discount", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Fruits", "2024-07-05", "Fresh apples", 10, "/images/apple.png", "Apple", 0.99m, 50 },
                    { 2, "Fruits", "2024-07-05", "Ripe bananas", 15, "/images/banana.png", "Banana", 0.59m, 100 },
                    { 3, "Fruits", "2024-07-05", "Juicy oranges", 5, "/images/orange.png", "Orange", 1.29m, 75 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "GroceryItem");
        }
    }
}
