using System.Reflection;
using ICS.App.Options;
using ICS.App.Services.Interfaces;
using ICS.BL;
using ICS.DAL;
using ICS.DAL.Migrator;
using ICS.DAL.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;
using WinUIEx;
    
[assembly:System.Resources.NeutralResourcesLanguage("en")]
namespace ICS.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        ConfigureAppSettings(builder);

        builder.Services
            .AddDALServices(GetDalOptions(builder.Configuration))
            .AddAppServices(GetAppOptions(builder.Configuration))
            .AddBLServices();

        builder.Services.AddTransient<IMauiInitializeService, DatabaseInitializer>();
        
        #if WINDOWS
                builder.ConfigureLifecycleEvents(events =>
                {
                    events.AddWindows(wndLifeCycleBuilder =>
                    {
                        wndLifeCycleBuilder.OnWindowCreated(window =>
                        {
                            window.Maximize();
                        });
                    });
                });
        #endif
        
        var app = builder.Build();
        
        // MigrateDb(app.Services.GetRequiredService<IDbMigrator>());
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());
        
        return app;
    }
    
    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        const string appSettingsFilePath = "ICS.App.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }
    
    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (var route in navigationService.Routes)
        {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }
    
    private static DalOptions GetDalOptions(IConfiguration configuration)
    {
        DalOptions dalOptions = new()
        {
            DatabaseDirectory = FileSystem.AppDataDirectory
        };
        configuration.GetSection("ICS:DAL").Bind(dalOptions);
        return dalOptions;
    }
    
    private static AppOptions GetAppOptions(IConfiguration configuration)
    {
        AppOptions appOptions = new();
        configuration.GetSection("ICS:App").Bind(appOptions);
        return appOptions;
    }
    
    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate();
}