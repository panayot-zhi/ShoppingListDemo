using ShoppingListDemo.Utility;
using Serilog;

namespace ShoppingListDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var services = builder.Services;
            var environment = builder.Environment;
            var host = builder.Host;

            host.UseSerilog((_, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(configuration));

            services.AddDatabase(configuration);
            services.AddControllersWithViews();
            services.AddRoutes();

            var app = builder.Build();

            if (!environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}