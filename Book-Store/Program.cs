using Book_Store.Models;
using Book_Store.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBookstoreRepository<Author>, AuthorDbRepository>();
builder.Services.AddScoped<IBookstoreRepository<Book>, BookDbRepository>();
builder.Services.AddDbContext<BookstoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Author}/{action=Index}/{id?}"
);


app.Run();
