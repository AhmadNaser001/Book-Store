using Book_Store.Models;
using Book_Store.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IBookstoreRepository<Author>, AuthorRepository>();
builder.Services.AddSingleton<IBookstoreRepository<Book>, BookRepository>();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Author}/{action=Index}/{id?}"
);


app.Run();
