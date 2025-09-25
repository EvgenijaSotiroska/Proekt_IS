using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoffeeEShop.Domain.Identity;
using CoffeeEShop.Repository;
using CoffeeEShop.Repository.Implementation;
using CoffeeEShop.Repository.Interface;
using CoffeeEShop.Service.Implementation;
using CoffeeEShop.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<SystemUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IShopService, ShopService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddHostedService<OrderStatusService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
