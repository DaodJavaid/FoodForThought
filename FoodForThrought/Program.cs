using FoodForThrought.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* Authentication Code*/
builder.Services.AddAuthentication()
    .AddCookie("AdminAuthentication", options =>
    {
        options.Cookie.Name = "AdminAuth";
        options.LoginPath = "/Admin/AdminLogin";
    })
    .AddCookie("ClientAuthentication", options =>
    {
        options.Cookie.Name = "ClientAuth";
        options.LoginPath = "/Home/Login";
    });

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

/* this Line AdminRegister Database */
builder.Services.AddDbContext<AdminDbContext>(options =>
options.UseSqlServer(connectionstring));

/* this Line Questions Database */
builder.Services.AddDbContext<QuestionnaireDbContext>(options =>
options.UseSqlServer(connectionstring));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
