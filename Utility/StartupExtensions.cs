using Microsoft.EntityFrameworkCore;
using ShoppingListDemo.Data;

namespace ShoppingListDemo.Utility;

public static class StartupExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqliteConnection") ??
                throw new InvalidOperationException("Connection string 'SqliteConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(
            contextLifetime: ServiceLifetime.Scoped,
            optionsAction: options =>
                options
                    .EnableDetailedErrors()
                    .UseSnakeCaseNamingConvention()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseSqlite(connectionString));
                    //.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        return services;
    }

    public static IServiceCollection AddRoutes(this IServiceCollection services)
    {
        services.AddRouting(option =>
        {
            option.LowercaseUrls = true;
        });

        return services;
    }
}