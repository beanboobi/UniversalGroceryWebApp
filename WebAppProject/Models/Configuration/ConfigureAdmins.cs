using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppProject.Models;

namespace WebAppProject.Models.Configuration
{
    internal class ConfigureAdmins : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Seed Admins
            builder.HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@example.com",
                    NormalizedEmail = "ADMIN1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password1"),
                    SecurityStamp = string.Empty
                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "admin2",
                    NormalizedUserName = "ADMIN2",
                    Email = "admin2@example.com",
                    NormalizedEmail = "ADMIN2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password2"),
                    SecurityStamp = string.Empty
                },
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "admin3",
                    NormalizedUserName = "ADMIN3",
                    Email = "admin3@example.com",
                    NormalizedEmail = "ADMIN3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password3"),
                    SecurityStamp = string.Empty
                }
            );
        }
    }
}