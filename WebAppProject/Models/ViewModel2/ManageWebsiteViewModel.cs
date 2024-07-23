using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class ManageWebsiteViewModel
    {
        public List<BannerImage> BannerImages { get; set; } = new List<BannerImage>();
        public BannerImage BannerImage { get; set; } = new BannerImage();
    }
}