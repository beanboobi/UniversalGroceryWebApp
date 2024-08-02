using WebAppProject.Models;
using System.Collections.Generic;

namespace WebAppProject.ViewModels
{
    public class ManageWebsiteViewModel
    {
        public List<BannerImage> MainBanners { get; set; } = new List<BannerImage>();
        public BannerImage BannerImage { get; set; } = new BannerImage(); // For form binding
    }
}