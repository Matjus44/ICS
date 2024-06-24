using ICS.Common.Enums;
using ICS.DAL;
using ICS.DAL.Entities;
using ICS.DAL.Options;
using ICS.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ICS.App;

public class DatabaseInitializer : IMauiInitializeService
{
    public void Initialize(IServiceProvider services)
    {
        var factory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        var options = services.GetRequiredService<DalOptions>();
        using (var dbContext = factory.CreateDbContext())
        {
            if (options.RecreateDatabaseEachTime)
            {
                dbContext.Database.EnsureDeleted();
            }
            
            dbContext.Database.EnsureCreated();

            if (options.SeedDemoData)
            {
                // Get collection with subjects, which contains activities
                ICollection<SubjectEntity> subjects = SubjectSeeds.Get();
                // Add subjects to the database
                dbContext.AddRange(subjects);
                
                
                // Get collection with students
                ICollection<StudentEntity> students = StudentSeeds.Get();
                // Add students to the database
                dbContext.AddRange(students);
                
                
                // M:N relation between students and subjects
                var random = new Random();
                
               
                foreach (var student in students)
                {
                    if (student.FirstName == "Mirko")
                    {
                        continue;
                    }
                    // Generate a random number of subjects for the student
                    int numSubjects = random.Next(1, subjects.Count + 1);
        
                    // Get a random subset of subjects
                    var selectedSubjects = subjects.OrderBy(s => random.Next()).Take(numSubjects).ToList();

                    foreach (var subject in selectedSubjects)
                    {
                        var studentSubject = new StudentsSubjectsEntity()
                        {
                            StudentId = student.Id,
                            SubjectId = subject.Id
                        };

                        student.RegisteredSubjects.Add(studentSubject);
                        dbContext.Add(studentSubject);
                    }
                }

                
                // Ratings need to be added after students and subjects manually
                
                foreach (var subject in subjects)
                {
                    foreach (var activity in subject.Activities)
                    {
                       
                        if (activity.Type != ActivityType.Exam) continue;

                        foreach (var student in subject.RegisteredStudents)
                        {
                            
                            dbContext.Add(new RatingEntity()
                            {
                                ActivityId = activity.Id,
                                StudentId = student.StudentId,
                                Rating = random.Next(1, 6), 
                                Note = "Grade for activity " + activity.ActivityDescription
                            });
                            dbContext.SaveChanges();  
                                
                        }
                    }
                }

            }
            
            dbContext.SaveChanges(); // Save changes to the database
        }
        
        
    }
    
}