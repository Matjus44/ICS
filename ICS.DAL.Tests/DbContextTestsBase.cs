using ICS.Common.Tests;
using ICS.Common.Tests.Factories;
using ICS.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ICS.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        AppDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<AppDbContext> DbContextFactory { get; }
    protected AppDbContext AppDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await AppDbContextSUT.Database.EnsureDeletedAsync();
        await AppDbContextSUT.Database.EnsureCreatedAsync();

        // APPLY DATABASE SEEDS

        SubjectSeeds.Seed(AppDbContextSUT);
        StudentSeeds.Seed(AppDbContextSUT);

        await AppDbContextSUT.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await AppDbContextSUT.Database.EnsureDeletedAsync();
        await AppDbContextSUT.DisposeAsync();
    }
}