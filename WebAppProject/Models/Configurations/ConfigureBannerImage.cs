using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppProject.Models;
using System;

namespace WebAppProject.Configurations
{
    internal class ConfigureBannerImage : IEntityTypeConfiguration<BannerImage>
    {
        public void Configure(EntityTypeBuilder<BannerImage> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.ImagePath)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.RedirectUrl)
                .HasMaxLength(255);

            builder.Property(b => b.CreatedDate)
                .IsRequired();

            builder.Property(b => b.BannerType)
                .IsRequired()
                .HasMaxLength(50);

            // Seeding initial data
            builder.HasData(
                new BannerImage
                {
                    Id = 1,
                    ImagePath = "/images/Websitebanner01.png",
                    RedirectUrl = "https://localhost:7065/Home/ProductCategory?category=Household",
                    CreatedDate = DateTime.Now,
                    BannerType = "MainBanner"
                },
                new BannerImage
                {
                    Id = 2,
                    ImagePath = "/images/Websitebanner02.png",
                    RedirectUrl = "https://localhost:7065/Home/ProductCategory?category=VegetablesAndFruit",
                    CreatedDate = DateTime.Now,
                    BannerType = "MainBanner"
                }
            );
        }
    }
}