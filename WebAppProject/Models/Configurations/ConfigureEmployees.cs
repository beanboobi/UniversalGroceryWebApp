//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using WebAppProject.Models;

//namespace WebAppProject.Configuration
//{
//    internal class ConfigureEmployee : IEntityTypeConfiguration<Employee>
//    {
//        public void Configure(EntityTypeBuilder<Employee> entity)
//        {
//            entity.HasData(
//                new Employee { Id = 6, Name = "user1", Email = "user1@example.com", Password = "password", JoinDate = DateTime.Now, Salary = 50000, Role = "Employee", ApplicationUserId = "6" },
//                new Employee { Id = 7, Name = "user2", Email = "user2@example.com", Password = "password", JoinDate = DateTime.Now, Salary = 60000, Role = "Admin", ApplicationUserId = "7" }
//            );
//        }
//    }
//}