using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string JoinDate { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string Role { get; set; } // To differentiate the admin or employee role

        public int UserId { get; set; } // Foreign key

        // Navigation property
        public User User { get; set; }
    }
}




