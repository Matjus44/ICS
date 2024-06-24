using ICS.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false)
    : IDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<AppDbContext> builder = new();
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");

        // builder.LogTo(Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new AppDbContext(builder.Options);
        //return new AppTestingDbContext(builder.Options, seedTestingData);
    }
}