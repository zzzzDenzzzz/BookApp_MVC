using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<BookDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


var app = builder.Build();


BookDbInitializer.Seed(app);


if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "admin",
    "{area}/{controller=Book}/{action=Index}");


app.MapControllerRoute(
    "default",
    "{controller=Book}/{action=Index}/{genreId?}/{authorId?}");

app.Run();