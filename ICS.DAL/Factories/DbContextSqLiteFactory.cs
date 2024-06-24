using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<AppDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<AppDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _seedTestingData = seedTestingData;

        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
        
        _contextOptionsBuilder.EnableSensitiveDataLogging();
        _contextOptionsBuilder.LogTo(Console.WriteLine);
    }

    public AppDbContext CreateDbContext() => new(_contextOptionsBuilder.Options, _seedTestingData);
}
