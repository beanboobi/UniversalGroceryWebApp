﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppProject.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeModelForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId1",
                table: "Employees",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId1",
                table: "Employees",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ApplicationUserId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId");
        }
    }
}
