using WebAppProject.Models;
using System.Collections.Generic;

namespace WebAppProject.ViewModels
{
    public class ManageWebsiteViewModel
    {
        public List<(BannerImage MainBanner, BannerImage SideBanner)> BannerPairs { get; set; } = new List<(BannerImage MainBanner, BannerImage SideBanner)>();
        public BannerImage BannerImage { get; set; } = new BannerImage(); // For form binding
    }
}