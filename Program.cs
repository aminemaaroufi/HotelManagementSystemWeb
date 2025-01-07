using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with MySQL connection
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("HotelDbContext"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("HotelDbContext"))
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
