using WebAppProject.Models;
using System.Collections.Generic;

namespace WebAppProject.ViewModels
{
    public class ManageWebsiteViewModel
    {
        public BannerImage MainBanner { get; set; } // Main Banner property
        public List<BannerImage> SideBanners { get; set; } = new List<BannerImage>(); // Side Banners property
        public BannerImage BannerImage { get; set; } = new BannerImage(); // For form binding
    }
}