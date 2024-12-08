using Microsoft.EntityFrameworkCore;
using TestWeb.DataAccess.Data;
using TestWeb.DataAccess.Repository;
using TestWeb.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// COnfigure options for AppDbContext
builder.Services.AddDbContext<AppDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(builder
    .Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UnitOfWork>();

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
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
