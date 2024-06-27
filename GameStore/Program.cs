using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(opts =>
{
	opts.UseSqlServer(
	builder.Configuration["ConnectionStrings:DefaultConnection"]);
});


builder.Services.AddTransient<IProduct, ProductRepository>();
builder.Services.AddTransient<ICategory, CategoryRepository>();
builder.Services.AddTransient<IOrder, OrderRepository>();


builder.Services.AddSession(options =>
{
	options.Cookie.Name = "GameStore.Session";
	options.IdleTimeout = System.TimeSpan.FromHours(48);
	options.Cookie.HttpOnly = false;
});


var app = builder.Build();

app.UseSession();

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
	pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();
