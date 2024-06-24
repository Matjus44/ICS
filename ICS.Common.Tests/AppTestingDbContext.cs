using ICS.Common.Tests.Seeds;
using ICS.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests;

public class AppTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
    : AppDbContext(contextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: here is bug
        if (seedTestingData)
        {
            // ActivitySeeds.Seed(modelBuilder);
            // RatingSeeds.Seed(modelBuilder);
            // StudentSeeds.Seed(modelBuilder);
            // StudentsSubjectsSeeds.Seed(modelBuilder);
            // SubjectSeeds.Seed(modelBuilder);
        }
    }
}