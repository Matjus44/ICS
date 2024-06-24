using ICS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Seeds;

public static class StudentSeeds
{
    public static ICollection<StudentEntity> Get()
    {
        return new List<StudentEntity>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Richard",
                LastName = "Smith",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/19.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/66.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/women/81.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Smith",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/women/18.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Marley",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/91.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Eve",
                LastName = "Bartley",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/women/57.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Charlie",
                LastName = "Karrey",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/57.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "David",
                LastName = "Morner",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/45.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Frank",
                LastName = "Smithson",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/78.jpg")
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mirko",
                LastName = "Haluska",
                PhotoUri = new Uri("https://randomuser.me/api/portraits/men/79.jpg")
            }
        };
    }

}