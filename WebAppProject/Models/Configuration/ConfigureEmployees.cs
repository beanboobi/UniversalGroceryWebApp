using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppProject.Models;

namespace WebAppProject.Models.Configuration
{
    internal class ConfigureEmployees : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Seed Employees
            builder.HasData(
                new Employee { Id = 1, Name = "Employee1", ApplicationUserId = "1" },
                new Employee { Id = 2, Name = "Employee2", ApplicationUserId = "2" },
                new Employee { Id = 3, Name = "Employee3", ApplicationUserId = "3" }
            );
        }
    }
}