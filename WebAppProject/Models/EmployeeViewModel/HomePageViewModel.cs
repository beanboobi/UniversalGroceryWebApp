using System.Collections.Generic;
using WebAppProject.Models;

/*
* Done By; Eugene 
* INFT 3050 Web Programming
* Comments: This is the Viewmodel to display the GorceryItems and the BannerImage in the index page (customer hoempage)
* Two Models are being combined into this viewmdoel as we can't use two models directly in the index page.
*/

namespace WebAppProject.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<GroceryItemViewModel> GroceryItems { get; set; }
        public List<BannerImage> MainBanners { get; set; } = new List<BannerImage>();
    }
}