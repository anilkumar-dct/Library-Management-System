using LibraryManagementSystemUsingMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemUsingMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Registering applicationdbcontext
            //Here we need to pass configration to options as constructor is expecting applicationdbcontext file.and here we are given with many option to configure for example usesql,or usememoryindatabase
            builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

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
                pattern: "{controller=Book}/{action=BookSection}/{id?}");

            app.Run();
        }
    }
}
