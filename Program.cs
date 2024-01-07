using ShoppingListDemo.Utility;
using Serilog;

namespace ShoppingListDemo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var services = builder.Services;
        var environment = builder.Environment;
        var host = builder.Host;

        host.UseSerilog(ConfigureLogger);

        services.AddDatabase(configuration);
        services.AddControllersWithViews();
        services.AddRoutes();

        var app = builder.Build();

        app.UseExceptionHandler("/Home/Error");

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }

    private static void ConfigureLogger(HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration)
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    }
}