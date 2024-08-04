using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class EmployeeViewModel
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
        public DateTime JoinDate { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string Role { get; set; }

        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }

}
