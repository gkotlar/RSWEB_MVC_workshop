using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookstoreApp.Models;
namespace BookstoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ReviewContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ReviewContext") ?? throw new InvalidOperationException("Connection string 'ReviewContext' not found.")));
            builder.Services.AddDbContext<BookContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookContext") ?? throw new InvalidOperationException("Connection string 'BookContext' not found.")));
            builder.Services.AddDbContext<GendreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GendreContext") ?? throw new InvalidOperationException("Connection string 'GendreContext' not found.")));
            builder.Services.AddDbContext<MVCAuthorContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MVCAuthorContext") ?? throw new InvalidOperationException("Connection string 'MVCAuthorContext' not found.")));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
