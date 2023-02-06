using FoodForThrought.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionstring = builder.Configuration.GetConnectionString("dbConnection");

// this Line Register User Data
builder.Services.AddDbContext<RegisterDbcontext>(options =>
options.UseSqlServer(connectionstring));

// this Line Message Database
builder.Services.AddDbContext<ContactDbcontext>(options =>
options.UseSqlServer(connectionstring));

// this Line Product Database
builder.Services.AddDbContext<ProductimageDbcontext>(options =>
options.UseSqlServer(connectionstring));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
