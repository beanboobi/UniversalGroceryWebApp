using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream:WebAppProject/Program.cs
using Microsoft.Extensions.DependencyInjection;
using WebAppProject.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebAppProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppProjectContext") ?? throw new InvalidOperationException("Connection string 'WebAppProjectContext' not found.")));
=======
using WebAppProject.Data;

var builder = WebApplication.CreateBuilder(args);
>>>>>>> Stashed changes:WebAppProject/WebAppProject/Program.cs

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
