using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppProject.Data;

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
        public string Password { get; set; } // Consider removing this or handling it differently

        [Required]
        public DateTime JoinDate { get; set; } // Changed from DateTime?

        [Required]
        public int Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string Role { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }  // Foreign key

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }  // Navigation property
    }
}



