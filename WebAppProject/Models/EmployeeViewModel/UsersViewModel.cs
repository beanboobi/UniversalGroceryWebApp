using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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

