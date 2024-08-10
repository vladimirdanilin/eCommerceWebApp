using ECommerceWebApp.Data;
using ECommerceWebApp.Data.Services;
using ECommerceWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Services Configuration
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//Adding DbSeedingService
builder.Services.AddHostedService<DatabaseSeedService>();

//Adding Identity Services
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    //options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Connection Configuration

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => //CookieAuthenticationOptions
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/AccessDenied");
    });

//Add policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireSuperAdminRole", policy => policy.RequireRole("SuperAdmin"));
    options.AddPolicy("RequireSalesManagerRole", policy => policy.RequireRole("SalesManager"));
    options.AddPolicy("RequireWarehouseManagerRole", policy => policy.RequireRole("WarehouseManager"));
    options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
});

// Add services to the container.
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

app.UseAuthentication();    // Authentication
app.UseAuthorization();     // Authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "shoppingCart",
    pattern: "{controller=ShoppingCart}/{action=AddItemToCart}");

app.Run();


