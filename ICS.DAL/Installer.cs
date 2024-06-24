using ICS.DAL.Entities;
using ICS.DAL.Factories;
using ICS.DAL.Mappers;
using ICS.DAL.Migrator;
using ICS.DAL.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ICS.DAL;

public static class Installer
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, DalOptions options)
    {
        //services.AddSingleton<IDbContextFactory<AppDbContext>>(_ =>
        //    new DbContextSqLiteFactory("test"));
        //services.AddSingleton<IDbMigrator, DbMigrator>();

        services.AddSingleton(options);

        if (options is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (string.IsNullOrEmpty(options.DatabaseDirectory))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseDirectory)} is not set");
        }
        if (string.IsNullOrEmpty(options.DatabaseName))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseName)} is not set");
        }

        services.AddSingleton<IDbContextFactory<AppDbContext>>(_ =>
            new DbContextSqLiteFactory(options.DatabaseFilePath, options?.SeedDemoData ?? false));
        services.AddSingleton<IDbMigrator, DbMigrator>();

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<RatingEntityMapper>();
        services.AddSingleton<StudentEntityMapper>();
        services.AddSingleton<SubjectEntityMapper>();
        services.AddSingleton<StudentsSubjectsEntityMapper>();

        return services;
    }
}