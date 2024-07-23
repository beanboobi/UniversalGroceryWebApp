using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models.ViewModel2
{
    public class GroceryBannerViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Category { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        public int Discount { get; set; }

        public decimal OriginalPrice { get; set; }

        [Required]
        public string CreatedDate { get; set; }

        public IEnumerable<BannerImage> BannerImages { get; set; }

        public BannerImage BannerImage { get; set; }

        [Required]
        [Display(Name = "Banner Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Redirect URL")]
        public string RedirectUrl { get; set; }


    }
}