using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace WebAppProject.Data
{
    public class WebAppProjectContext : DbContext
    {
        public WebAppProjectContext (DbContextOptions<WebAppProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Product.Models.testproduct> testproduct { get; set; } = default!;
    }
}
