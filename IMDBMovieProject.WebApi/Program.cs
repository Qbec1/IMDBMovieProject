using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ApiServices>();


builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Imdb.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.IOTimeout = TimeSpan.FromMinutes(10);
});


builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Account/SignIn";
        x.AccessDeniedPath = "/AccessDenied";
        x.Cookie.Name="Account";
        x.Cookie.MaxAge = TimeSpan.FromDays(7);
        x.Cookie.IsEssential = true; 
    });


builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireRole(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireRole(ClaimTypes.Role, "User", "Admin", "Customer"));

});
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
app.UseSession(); //session kullanýmý

app.UseAuthentication(); //login
app.UseAuthorization(); //yetki
app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
