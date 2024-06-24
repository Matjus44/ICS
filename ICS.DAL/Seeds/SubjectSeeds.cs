using ICS.Common.Enums;
using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Seeds;

public static class SubjectSeeds
{
   public static ICollection<SubjectEntity> Get()
   {
       return new List<SubjectEntity>()
       {
           new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A95"),
               Name = "Seminar C#",
               CodeName = "ICS",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 1",
                       Start = new DateTime(2023, 10, 1, 9, 0, 0),
                       Finish = new DateTime(2023, 10, 1, 11, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A95"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 3",
                       Start = new DateTime(2023, 10, 5, 12, 0, 0),
                       Finish = new DateTime(2023, 10, 5, 14, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A95"),
                       Room = RoomName.D0207,
                       Type = ActivityType.Lecture
                   },
               }
           },
           new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B50"),
                Name = "Seminar Java",
                CodeName = "IJA",
                Activities = new List<ActivityEntity>()
                {
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        ActivityDescription = "Lecture 1",
                        Start = new DateTime(2023, 10, 2, 9, 0, 0),
                        Finish = new DateTime(2023, 10, 2, 11, 0, 0),
                        SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B50"),
                        Room = RoomName.D105,
                        Type = ActivityType.Lecture
                    }, new ()
                    {
                        Id = Guid.NewGuid(),
                        ActivityDescription = "Exam 1",
                        Start = new DateTime(2023, 10, 2, 9, 0, 0),
                        Finish = new DateTime(2023, 10, 2, 11, 0, 0),
                        SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B50"),
                        Room = RoomName.D105,
                        Type = ActivityType.Exam
                    }
                }
           }, new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B51"),
               Name = "Operating Systems",
               CodeName = "IOS",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 1",
                       Start = new DateTime(2023, 10, 4, 15, 0, 0),
                       Finish = new DateTime(2023, 10, 4, 18, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B51"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },  new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Exam 1",
                       Start = new DateTime(2023, 10, 5, 15, 0, 0),
                       Finish = new DateTime(2023, 10, 5, 18, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B51"),
                       Room = RoomName.D105,
                       Type = ActivityType.Exam
                   }
               }
           }, new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B52"),
               Name = "Mathematical Analysis",
               CodeName = "IMA",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 1",
                       Start = new DateTime(2023, 10, 5, 9, 0, 0),
                       Finish = new DateTime(2023, 10, 5, 11, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B52"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Exam 1",
                       Start = new DateTime(2023, 10, 1, 15, 0, 0),
                       Finish = new DateTime(2023, 10, 1, 16, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B52"),
                       Room = RoomName.D105,
                       Type = ActivityType.Exam
                   }
               }
           }, new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B53"),
               Name = "Theoretical Informatics",
               CodeName = "TIN",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 1",
                       Start = new DateTime(2023, 10, 5, 15, 0, 0),
                       Finish = new DateTime(2023, 10, 5, 17, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B53"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 2",
                       Start = new DateTime(2023, 10, 2, 14, 0, 0),
                       Finish = new DateTime(2023, 10, 2, 16, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B53"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Exam 1",
                       Start = new DateTime(2023, 10, 2, 14, 0, 0),
                       Finish = new DateTime(2023, 10, 2, 16, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B53"),
                       Room = RoomName.D105,
                       Type = ActivityType.Exam
                   }
               }
            }, new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B55"),
               Name = "C++ Seminar",
               CodeName = "ICP",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 1",
                       Start = new DateTime(2023, 10, 3, 16, 0, 0),
                       Finish = new DateTime(2023, 10, 3, 18, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B55"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Exam 2",
                       Start = new DateTime(2023, 10, 2, 14, 0, 0),
                       Finish = new DateTime(2023, 10, 2, 16, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095B55"),
                       Room = RoomName.D105,
                       Type = ActivityType.Exam
                   }
               }
           },
           new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A96"),
               Name = "The C Programming Language",
               CodeName = "IJC",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 2",
                       Start = new DateTime(2023, 10, 3, 12, 0, 0),
                       Finish = new DateTime(2023, 10, 3, 15, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A96"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Exam 1",
                       Start = new DateTime(2023, 10, 3, 12, 0, 0),
                       Finish = new DateTime(2023, 10, 3, 15, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A96"),
                       Room = RoomName.D105,
                       Type = ActivityType.Exam
                   }
                   // new()
                   // {
                   //     Id = Guid.NewGuid(),
                   //     ActivityDescription = "Lecture 3",
                   //     Start = new DateTime(2023, 10, 5, 13, 0, 0),
                   //     Finish = new DateTime(2023, 10, 5, 15, 0, 0),
                   //     SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A96"),
                   //     Room = RoomName.D0206,
                   //     Type = ActivityType.Exam
                   // }
               }
           },
           new SubjectEntity()
           {
               Id = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A97"),
               Name = "Scripting Languages",
               CodeName = "ISJ",
               Activities = new List<ActivityEntity>()
               {
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 1",
                       Start = new DateTime(2023, 10, 2, 11, 0, 0),
                       Finish = new DateTime(2023, 10, 2, 13, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A97"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },
                   new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Lecture 2",
                       Start = new DateTime(2023, 10, 4, 11, 0, 0),
                       Finish = new DateTime(2023, 10, 4, 13, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A97"),
                       Room = RoomName.D105,
                       Type = ActivityType.Lecture
                   },new()
                   {
                       Id = Guid.NewGuid(),
                       ActivityDescription = "Exam 1",
                       Start = new DateTime(2023, 10, 4, 11, 0, 0),
                       Finish = new DateTime(2023, 10, 4, 13, 0, 0),
                       SubjectId = Guid.Parse("7E14BC4E-283B-4EC6-8166-07CDAA095A97"),
                       Room = RoomName.D105,
                       Type = ActivityType.Exam
                   }
               }
           }
       };
   }
}
