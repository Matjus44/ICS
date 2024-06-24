using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICS.DAL.Entities;

namespace ICS.DAL.Mappers;

public class StudentEntityMapper : IEntityMapper<StudentEntity>
{
    public void MapToExistingEntity(StudentEntity existingEntity, StudentEntity updatedEntity)
    {
        existingEntity.FirstName = updatedEntity.FirstName;
        existingEntity.LastName = updatedEntity.LastName;
        existingEntity.PhotoUri = updatedEntity.PhotoUri;
    }
}
