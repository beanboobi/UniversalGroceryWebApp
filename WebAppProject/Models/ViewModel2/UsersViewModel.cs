using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels

{
    public class UsersViewModel
    {
        [Required]
        public string Id { get; set; }  // Identity uses string IDs by default

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // Consider security implications of handling passwords

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public List<string> Role { get; set; } // To handle multiple roles
    }
}

