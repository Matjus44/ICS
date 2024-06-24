using CommunityToolkit.Mvvm.Messaging;
using ICS.App.Options;
using ICS.App.Services;
using ICS.App.Services.Interfaces;
using ICS.App.ViewModels;
using ICS.App.Views;

namespace ICS.App;

public static class AppInstaller
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, AppOptions options)
    {
        services.AddSingleton<AppShell>();
        services.AddSingleton(options);

        services.AddSingleton<IMessenger>(_ => StrongReferenceMessenger.Default);
        services.AddSingleton<IMessengerService, MessengerService>();
        services.AddSingleton<IAlertService, AlertService>();

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
            .AsSelf()
            .WithTransientLifetime());

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<IViewModel>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());

        services.AddTransient<INavigationService, NavigationService>();

        return services;
    }
}