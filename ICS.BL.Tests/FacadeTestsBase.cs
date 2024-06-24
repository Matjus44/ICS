using ICS.BL.Mappers;
using ICS.BL.Mappers.Interfaces;
using ICS.Common.Tests;
using ICS.Common.Tests.Factories;
using ICS.Common.Tests.Seeds;
using ICS.DAL;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ICS.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        StudentModelMapper = new StudentModelMapper();
        RatingModelMapper = new RatingModelMapper();
        ActivityModelMapper = new ActivityModelMapper(RatingModelMapper);
        SubjectModelMapper = new SubjectModelMapper(ActivityModelMapper);
        StudentsSubjectsModelMapper = new StudentsSubjectsModelMapper(StudentModelMapper, SubjectModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<AppDbContext> DbContextFactory { get; }

    protected IStudentModelMapper StudentModelMapper { get; }
    protected ISubjectModelMapper SubjectModelMapper { get; }
    protected IRatingModelMapper RatingModelMapper { get; }
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IStudentsSubjectsModelMapper StudentsSubjectsModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
        SubjectSeeds.Seed(dbx);
        StudentSeeds.Seed(dbx);
        await dbx.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}