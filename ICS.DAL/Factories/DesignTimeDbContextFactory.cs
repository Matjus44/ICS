using Microsoft.EntityFrameworkCore.Design;

namespace ICS.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new($"Data Source=ICS_Proj;Cache=Shared");

    public AppDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}
