using System.Collections.Generic;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<GroceryItemViewModel> GroceryItems { get; set; }
        public BannerImage MainBanner { get; set; } // Add this property
        public IEnumerable<BannerImage> SideBanners { get; set; } // Modify this property
    }
}
