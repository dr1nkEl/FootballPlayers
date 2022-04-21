using WEB.Infrastructure.Initializers;
using Infrastructure.DataAccess;
using WEB.Infrastructure.Startup;
using Infrastructure.Abstractions;
using WEB.Infrastructure.MappingProfiles;
using MediatR;
using WEB.Infrastructure.Middleware;
using UseCases.Players;
using WEB.Infrastructure.Models;
using Infrastructure.SignalR;

namespace Saritasa.People.Web;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Entry point for web application.
    /// </summary>
    /// <param name="configuration">Global configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    /// <param name="environment">Application environment.</param>
    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
    {
        // Add controllers.
        services.AddControllers();
        services.AddControllersWithViews();

        // Database.
        services.AddDbContext<AppDbContext>(
            new DbContextOptionsSetup(configuration.GetConnectionString("AppDatabase")).Setup);
        services.AddAsyncInitializer<DatabaseInitializer>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        // Swagger.
        services.AddSwaggerGen();

        // Add authentication.
        services.AddAuthentication();

        // Add authorization
        services.AddAuthorization();

        // Automapper.
        services.AddAutoMapper(typeof(PlayerMappingProfile).Assembly);

        // MediatR.
        services.AddMediatR(typeof(CreatePlayerCommand).Assembly);

        // SignalR
        services.AddSignalR();
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseHsts();
        }
        app.UseMiddleware<ApiExceptionMiddleware>();
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapSwagger();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Player}/{action=List}/{id?}");
            endpoints.MapControllers();
            endpoints.MapHub<PlayerHub>(HubRoutes.Player);
        });
    }
}
