using System.Collections.Generic;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<GroceryItemViewModel> GroceryItems { get; set; }
        public List<BannerImage> MainBanners { get; set; } = new List<BannerImage>();
        public List<BannerImage> SideBanners { get; set; } = new List<BannerImage>();

        public IEnumerable<(BannerImage Main, BannerImage Side)> BannerPairs()
        {
            var mainBannersArray = MainBanners.ToArray();
            if (mainBannersArray.Length == 0)
            {
                // No main banners to pair with
                foreach (var sideBanner in SideBanners)
                {
                    yield return (null, sideBanner);
                }
            }
            else
            {
                int mainBannerIndex = 0;
                foreach (var sideBanner in SideBanners)
                {
                    yield return (mainBannersArray[mainBannerIndex], sideBanner);
                    mainBannerIndex = (mainBannerIndex + 1) % mainBannersArray.Length;
                }
            }
        }
    }
}