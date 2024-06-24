using System.Diagnostics;
using ICS.DAL.Entities;
using ICS.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL;

public class AppDbContext(DbContextOptions options, bool seedDemoData = false) : DbContext(options)
{
    public DbSet<ActivityEntity> ActivityEntities => Set<ActivityEntity>();
    public DbSet<StudentEntity> StudentEntities => Set<StudentEntity>();
    public DbSet<SubjectEntity> SubjectEntities => Set<SubjectEntity>();
    public DbSet<RatingEntity> RatingEntities => Set<RatingEntity>();
    public DbSet<StudentsSubjectsEntity> StudentsSubjectsEntities => Set<StudentsSubjectsEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentsSubjectsEntity>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.RegisteredSubjects);

        modelBuilder.Entity<StudentsSubjectsEntity>()
            .HasOne(ss => ss.Subject)
            .WithMany(ss => ss.RegisteredStudents);

        modelBuilder.Entity<ActivityEntity>()
            .HasMany(a => a.Ratings)
            .WithOne(a => a.Activity)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RatingEntity>()
            .HasOne(r => r.Activity)
            .WithMany(r => r.Ratings)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentEntity>()
            .HasMany(s => s.RegisteredSubjects)
            .WithOne(s => s.Student)
            .OnDelete(DeleteBehavior.Cascade);

        // if (seedDemoData)
        // {
        //     ActivitySeeds.Seed(modelBuilder);
        //     RatingSeeds.Seed(modelBuilder);
        //     StudentSeeds.Seed(modelBuilder);
        //     SubjectSeeds.Seed(modelBuilder);
        //     StudentsSubjectsSeeds.Seed(modelBuilder);
        // }
    }
}