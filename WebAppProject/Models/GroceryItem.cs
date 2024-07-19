using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class GroceryItem
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

        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public String CreatedDate { get; set; }

        [Required]
        public String Category { get; set; }

    }
}