using System.Collections.Generic;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<GroceryItemViewModel> GroceryItems { get; set; }
        public IEnumerable<BannerImage> BannerImages { get; set; }
    }
}
