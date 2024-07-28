using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class BannerImage
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Banner Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Redirect URL")]
        public string RedirectUrl { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        public string BannerType { get; set; } 

    }

}



