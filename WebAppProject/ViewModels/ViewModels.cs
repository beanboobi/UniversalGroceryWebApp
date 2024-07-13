namespace WebAppProject.ViewModels
{
    public class GroceryItemViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}

