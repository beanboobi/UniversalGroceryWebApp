using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppProject.Models;
using System;

namespace WebAppProject.Configurations
{
    internal class ConfigureBannerImage : IEntityTypeConfiguration<BannerImage>
    {
        public void Configure(EntityTypeBuilder<BannerImage> entity)
        {
            entity.HasData(
                new BannerImage { Id = 1, ImagePath = "/images/BannerImage(Cropped).jpg", RedirectUrl = "https://example.com", CreatedDate = DateTime.Now, BannerType = "MainBanners" },
                new BannerImage { Id = 2, ImagePath = "/images/vector-big-sale-banner.jpg", RedirectUrl = "https://example.com", CreatedDate = DateTime.Now, BannerType = "MainBanners" }
               

            );
        }
    }
}