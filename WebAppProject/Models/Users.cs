<<<<<<< HEAD:WebAppProject/WebAppProject/Models/Users.cs
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Role { get; set; } // To differentiate between Employee, Admin, and Customer

        // Navigation property
        public ICollection<Employee> Employees { get; set; }
    }
}


=======
﻿using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class Users
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
>>>>>>> origin/Kaiboon2:WebAppProject/Models/Users.cs
