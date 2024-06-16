using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class Users
    {
        public int ID { get; set; }
       
        public string Name { get; set; }
        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }

    }
}
