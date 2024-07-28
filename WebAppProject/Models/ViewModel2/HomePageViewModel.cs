using System.Collections.Generic;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<GroceryItemViewModel> GroceryItems { get; set; }
        public List<BannerImage> MainBanners { get; set; } = new List<BannerImage>(); // Modify this property
        public List<BannerImage> SideBanners { get; set; } = new List<BannerImage>(); // Modify this property

        // Creates pairs of MainBanner with each SideBanner
        public IEnumerable<(BannerImage Main, BannerImage Side)> BannerPairs()
        {
            var mainBannersArray = MainBanners.ToArray();
            int mainBannerIndex = 0;
            foreach (var sideBanner in SideBanners)
            {
                yield return (mainBannersArray[mainBannerIndex], sideBanner);
                mainBannerIndex = (mainBannerIndex + 1) % mainBannersArray.Length;
            }
        }
    }
}