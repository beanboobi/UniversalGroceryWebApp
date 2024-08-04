using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppProject.Models;


/* 
 * Done By: Eugene
 * INFT 3050 Web Programming
 * Comments: This is the EmployeeViewModel, it connects the ManageEmployee view page to the Employee model & database and the Aspnetusers 
 * database. Every employee added will also be added to the aspnetusers table, the viewmodel supports the admin actions of adding,
 * editing and deleting of employee records
 * 
 */

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
