using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WebAppProject.Models;

/*
 * Done By: Eugene 
 * INFT 3050 Web Programming
 * Comments: This is the UsersViewModel which connects the ManageCustomerAcc page to the ASPNetUsers Table, it allows actions 
 * like adding, editing and deleting of Customer records in the ASPNetUsers Table.
 */


namespace WebAppProject.ViewModels

{
    public class UsersViewModel
    {
        [Required]
        public string Id { get; set; }  // Identity uses string IDs by default

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [AllowNull]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // Consider security implications of handling passwords

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public string PrimaryRole => Roles.FirstOrDefault() ?? "User";
    }
}

