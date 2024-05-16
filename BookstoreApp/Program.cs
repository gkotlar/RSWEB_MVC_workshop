using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookstoreApp.Models;
using BookstoreApp.Data;
using Microsoft.AspNetCore.Identity;
namespace BookstoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BookstoreAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookstoreAppContext") ?? throw new InvalidOperationException("Connection string 'BookstoreAppContext' not found.")));

            builder.Services.AddDefaultIdentity<BookstoreAppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BookstoreAppContext>();

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
