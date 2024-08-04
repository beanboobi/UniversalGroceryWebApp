/*
* Done By: Eugene 
* INFT 3050 Web Programming
* Comments: This is the ViewModel for the admin side that connects the Admin ManageWebstie Page witht the BannerImage model
* to allow actions like adding, deleting or editing of bannerimages stored inside the bannerimage database.
*/


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