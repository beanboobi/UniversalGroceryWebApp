using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
 
   public class ApplicationUser : IdentityUser
    {
       
        public virtual ICollection<Employee> Employees { get; set; }  // Navigation property if needed
    }
}




